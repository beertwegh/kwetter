using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Helpers;
using AuthService.Models;

namespace AuthService.Services
{
    public interface IAuthService
    {
        Task<string> Authenticate(AuthUser authUser);
        bool ValidateToken(string token);
        string GetClaim(string token, string claimType = Globals.UserIdClaim);
        void UserRegistered(AuthUser user);
        void UserDeleted(Guid UserId);
    }
}
