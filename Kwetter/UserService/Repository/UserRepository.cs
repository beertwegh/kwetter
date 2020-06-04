using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserService.DbContext;
using UserService.Models;
using UserService.Repository.Interface;

namespace UserService.Repository
{
    public class UserRepository: IUserRepository

    {
        private readonly UserDbContext _userDbContext;

        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async void Register(UserRegistrationModel registerUser)
        {
            var user = new User(registerUser.Email, registerUser.Location, registerUser.Web, registerUser.Bio, registerUser.UserId);
            await _userDbContext.AddAsync(user);
            await _userDbContext.SaveChangesAsync();
        }

        public async void EditUser(User userUpdate)
        {
            var user = _userDbContext.Users.Single(u => u.UserId == userUpdate.UserId);
            /*_userDbContext.Entry(user.Bio).CurrentValues.SetValues(userUpdate.Bio);
            _userDbContext.Entry(user.Web).CurrentValues.SetValues(userUpdate.Web);
            _userDbContext.Entry(user.Location).CurrentValues.SetValues(userUpdate.Location);*/
            user.Bio = userUpdate.Bio;
            user.Location = userUpdate.Location;
            user.Web = userUpdate.Web;
            await _userDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserDetails(Guid userId)
        {
            var user = _userDbContext.Users.Single(u => u.UserId == userId);
            return user;

        }
    }
}
