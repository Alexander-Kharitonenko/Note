using AutoMapper;
using Commands.Users.Update;
using Commands.Users.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Note.API.View.Users;
using Queries.Users;
using Commands.Users.Delete;
using Microsoft.AspNetCore.Authorization;
using Note.API.Services.ViewMapper;
using Note.API.View.Base;
using Note.API.View.Shared;
using NSwag.Annotations;
using Microsoft.AspNetCore.OData.Query;
using Notes.Domain.Users;

namespace Note.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IViewMapperService _viewMapper;
        public UserController(IMediator mediator, IMapper mapper, IViewMapperService viewMapper)
        {
            _mediator = mediator;
            _mapper = mapper;
            _viewMapper = viewMapper;
        }

        [HttpGet]
        public async Task<ActionResult<ViewListModelDto<ViewUserModelDto>>> GetAll([OpenApiIgnore]ODataQueryOptions<UserModel> options ) 
        {
            
            var query = new FullUserListQuery() { options = options};

            var data = await _mediator.Send(query);

            return _viewMapper.ShowResult<ViewUserModelDto, UserModel>(data);
        }

        [HttpGet("{id}")]
        public async Task<ViewUserModelDto> Get(ulong id) 
        {
            var query = new FindUserQuery()
            {
                Id = id
            };

            var user = await _mediator.Send(query);

            var result = _mapper.Map<ViewUserModelDto>(user);

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<DoneResult>> Create(CreateUserDto user)
        {
            var query = _mapper.Map<CreateUserCommand>(user);

            var data = await _mediator.Send(query);

            return _viewMapper.ShowResult<DoneResult>(data);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<DoneResult>> Patch(ulong id, UpdateUserDto user)
        {
            var query = _mapper.Map<UpdateUserCommand>(user);

            query.UserId = id;

            var data = await _mediator.Send(query);

            return _viewMapper.ShowResult<DoneResult>(data);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<DoneResult>> Delete(ulong id)
        {
            var query = new DeleteUserCommand(){ Id = id };

            var data = await _mediator.Send(query);

            return _viewMapper.ShowResult<DoneResult>(data);
        }
    }
}
