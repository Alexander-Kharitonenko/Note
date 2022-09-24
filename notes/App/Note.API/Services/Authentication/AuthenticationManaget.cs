using AutoMapper;
using Commands.Users.Create;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Note.API.Services.Authentication.Access;
using Note.API.View.Auth;
using Note.API.View.Users;
using Notes.Domain.Interfaces;
using Notes.Domain.Users;
using Notes.Environment.SystemConstants;
using Queries.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Note.API.Services.Authentication
{
    public class AuthenticationManaget : IAuthenticationManaget
    {
        private readonly IOptions<AuthOptions> _options;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
    
        public AuthenticationManaget(
			IOptions<AuthOptions> options,
			IMediator mediator,
		IMapper mapper
			)
        {
			_mediator = mediator;
			_options = options;
			_mapper = mapper;
        }

        public async Task<string> AuthenticateAsync(ViewLoginModel query)
		{	

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

        public Task<string> GenerateRefreshToken()
        {
            throw new NotImplementedException();
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
    }
}
