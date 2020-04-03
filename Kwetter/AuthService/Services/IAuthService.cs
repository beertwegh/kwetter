using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Models;

namespace AuthService.Services
{
    public interface IAuthService
    {
        Task<string> Authenticate(AuthUser authUser);
    }
}
