using AutoMapper;
using Commands.Users.Create;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.UriParser;
using Note.API.Services.Authentication.Access;
using Note.API.View.Auth;
using Note.API.View.Users;
using Notes.Domain.Interfaces;
using Notes.Domain.Users;
using Notes.Environment.Commands;
using Notes.Environment.SystemConstants;
using Queries.Notes;
using Queries.Users;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Note.API.Services.Authentication
{
    public class AuthenticationManaget : IAuthenticationManaget
    {
        private readonly IOptions<AuthOptions> _options;
        private readonly IValidator<ViewLoginModel> _loginValidator;
        private readonly IValidator<ViewRefreshTokenModel> _refreshTokenValidator;
        private readonly IMediator _mediator;
		private readonly IMapper _mapper;
    
        public AuthenticationManaget(
			IOptions<AuthOptions> options,
            IValidator<ViewLoginModel> loginValidator,
            IValidator<ViewRefreshTokenModel> refreshTokenValidator,
            IMediator mediator,
		    IMapper mapper
		)
        {
            _options = options;
			_loginValidator = loginValidator;
            _refreshTokenValidator = refreshTokenValidator;
			_mediator = mediator;		
			_mapper = mapper;
        }

        public async Task<string> AuthenticateAsync(ViewLoginModel query)
		{
			if (!_loginValidator.Validate(query).IsValid) 
			{
				return null;
			}

            var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(_options.Value.Key);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Email, query.Email),
				    new Claim("role", AuthorizationRole.User.ToString())
				}),
				Expires = DateTime.UtcNow.AddMinutes(_options.Value.TokenLifetime),
				Issuer = _options.Value.Issuer,
				Audience = _options.Value.Audience,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

        public async Task<string> RegistrationAsync(ViewRegisterModel query)
        {
			var allUsers = await _mediator.Send(new FullUserListQuery());

			var password = Convert.ToBase64String(Encoding.UTF8.GetBytes(query.Password));

			var user = allUsers.Data.SingleOrDefault(el => el.Password == password && el.Email == query.Email);

			if (user != null)
			{
				throw new Exception("This user is registered");
			}

			var newUser = new CreateUserCommand()
			{
				Email = query.Email,
				Password = query.Password,
				Login = query.Login,
			};

			var result = await _mediator.Send(newUser);

			if (result.State == StateType.error) 
			{
				return null;
			}

			var model = new ViewLoginModel()
			{
				Email = query.Email,
				Password = query.Password,
			};

			var token =  await AuthenticateAsync(model);

			return token;

		}

		public async Task<string> RefreshToken(ViewRefreshTokenModel data, HttpContext httpContext ) 
		{

            if (!_refreshTokenValidator.Validate(data).IsValid)
            {
                return null;
            }

            var queryString = httpContext.Request.QueryString.Value?.Replace(httpContext.Request.QueryString.Value, $"?$filter=Email eq '{data.Email}'");
            httpContext.Request.QueryString = new QueryString(queryString);

            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.AddEntityType(typeof(UserModel));
            var edmModel = modelBuilder.GetEdmModel();
            var context = new ODataQueryContext(edmModel, typeof(UserModel), new ODataPath());
            var oDataQueryOptions = new ODataQueryOptions<UserModel>(context, httpContext.Request);

            var query = new FullUserListQuery()
            {
                options = oDataQueryOptions
            };

            var user = _mediator.Send(query).Result.Data.FirstOrDefault();

            if (user == null)
            {
				return null;
            }

			var liginModel = _mapper.Map<ViewLoginModel>(user);

			var token = await AuthenticateAsync(liginModel);

			return token;
        }
    }
}
