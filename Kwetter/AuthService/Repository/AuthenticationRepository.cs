using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.DbContext;
using AuthService.Models;
using AuthService.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repository
{
    internal class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AuthContext _authContext;

        public AuthenticationRepository(AuthContext authContext)
        {
            _authContext = authContext;
        }

        public async Task<AuthUser> ValidateAuthUser(string authUserUsername, string password)
        {

            var user = await _authContext.AuthUsers.SingleOrDefaultAsync(u => u.Username == authUserUsername);
            return BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;
        }

        public void SaveNewUser(AuthUser user)
        {
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _authContext.AuthUsers.Add(user);
                _authContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UserDeleted(Guid userId)
        {
            var user = _authContext.AuthUsers.Single(u => u.UserId == userId);
            _authContext.AuthUsers.Remove(user);
            _authContext.SaveChanges();
        }
    }
}
