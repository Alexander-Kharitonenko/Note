using Note.API.View.Auth;
using Note.API.View.Users;
using Queries.Users;
using System.IdentityModel.Tokens.Jwt;

namespace Note.API.Services.Authentication
{
    public interface IAuthenticationManaget
    {
        public Task<string> AuthenticateAsync(ViewLoginModel user);
        public Task<string> RegistrationAsync(ViewRegisterModel user);
        public Task<string> RefreshToken(ViewRefreshTokenModel email, HttpContext httpContext);
    }
}
