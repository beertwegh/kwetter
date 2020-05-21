using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Repository.Interface
{
    public interface IUserRepository
    {
        void Register(UserRegistrationModel registerUser);
        void EditBio(Guid userId, String bio);
        Task<User> GetUserDetails(Guid userId);
    }
}
