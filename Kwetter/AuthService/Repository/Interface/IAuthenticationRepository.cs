using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Models;

namespace AuthService.Repository.Interface
{

    public interface IAuthenticationRepository
    {

        Task<AuthUser> ValidateAuthUser(string authUserUsername, string password);
    }
}
