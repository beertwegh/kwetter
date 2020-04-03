using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthService.Helpers;
using AuthService.Models;
using AuthService.Repository.Interface;
using Microsoft.IdentityModel.Tokens;
namespace AuthService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthenticationRepository _authRepo;
        private readonly AppSettings _appSettings;
        public AuthService(IAuthenticationRepository authRepo, AppSettings appSettings)
        {
            _authRepo = authRepo;
            _appSettings = appSettings;
        }

        public async Task<string> Authenticate(AuthUser authUser)
        {
            var user = await _authRepo.ValidateAuthUser(authUser.Username, authUser.Password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
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
