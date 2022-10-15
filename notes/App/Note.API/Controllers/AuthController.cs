using AutoMapper;
using Commands.Users.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Note.API.Services.Authentication;
using Note.API.View.Auth;
using Note.API.View.Users;
using Queries.Notes;
using Queries.Users;
using Notes.Domain.Notes;
using Note.API.Services.ViewMapper;
using Notes.Environment.Commands;
using Environment = Notes.Environment.SystemConstants;
using System.Text;
using Notes.Environment.Queries;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Note.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAuthenticationManaget _authentication;
        private readonly IViewMapperService _viewMapper;

        public AuthController(
            IMediator mediator, 
            IMapper mapper, 
            IAuthenticationManaget authentication, 
            IViewMapperService viewMapper) 
        {
            _mediator = mediator;
            _mapper = mapper;
            _authentication = authentication;
            _viewMapper = viewMapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<string>> Registration([FromBody]ViewRegisterModel query) 
        {

            var token = await _authentication.RegistrationAsync(query);

            if (token == null) 
            {
                var result = CommandState.Error(Environment.StatusCode.NotRegistered);
                return _viewMapper.ShowError(result);
            }

            var access_toke = new QueryState<string>() { Data = token };
       
            return _viewMapper.ShowResult<string, string>(access_toke);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login([FromBody]ViewLoginModel model)
        {
       
            var query = new FullUserListQuery(){};

            var password = Convert.ToBase64String(Encoding.UTF8.GetBytes(model.Password));

            var users = _mediator.Send(query).Result.Data.SingleOrDefault(el => el.Password == password && el.Email == model.Email);

            if (users == null) 
            {
                var result = CommandState.Error(Environment.StatusCode.NotAuthorized);
                return _viewMapper.ShowError(result);
            }

            var token = await _authentication.AuthenticateAsync(model);


            if (token == null)
            {
                var result = CommandState.Error(Environment.StatusCode.NotAuthorized);
                return _viewMapper.ShowError(result);
            }

            var access_toke = new QueryState<string>() { Data = token };

            return _viewMapper.ShowResult<string, string>(access_toke);
        }

        [HttpPost]
        [Route("refreshToken")]
        public async Task<ActionResult<string>> RefreshToken(string email)
        {
            var data = new ViewRefreshTokenModel()
            {
                Email = email
            };
            var token = await _authentication.RefreshToken(data, HttpContext);

            if (token == null) 
            {
                var result = CommandState.Error(Environment.StatusCode.NotAuthorized);
                return _viewMapper.ShowError(result);
            }

            var access_toke = new QueryState<string>() { Data = token };

            return _viewMapper.ShowResult<string, string>(access_toke);

        }

    }
}
