using AuthService.Models;
using AuthService.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthenticationRepository _authRepo;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthenticationRepository authRepo, IConfiguration configuration)
        {
            _authRepo = authRepo;
            this._configuration = configuration;
        }

        public async Task<string> Authenticate(AuthUser authUser)
        {
            var user = await _authRepo.ValidateAuthUser(authUser.Username, authUser.Password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration.GetValue<string>("AppSettings:Secret");
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
