﻿using System;
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
            var user = await _authContext.AuthUsers.SingleOrDefaultAsync(u => u.Username == authUserUsername && u.Password == password);
            return user;
        }

        public async Task SaveNewUser(AuthUser user)
        {
            await _authContext.AuthUsers.AddAsync(user);
            await _authContext.SaveChangesAsync();

        }
    }
}
