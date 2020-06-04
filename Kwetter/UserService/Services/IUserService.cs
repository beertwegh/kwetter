using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageService.Models;

namespace MessageService.Services
{
    public interface IUserService
    {
        void Registration(UserRegistrationModel model);
        Task<User> GetUserDetails(Guid userId);
        void EditUser(User user);
    }
}
