using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageService.Models;

namespace MessageService.Repository.Interface
{
    public interface IUserRepository
    {
        void Register(UserRegistrationModel registerUser);
        Task<User> GetUserDetails(Guid userId);
        void EditUser(User userUpdate);
    }
}
