using System;
using System.Linq;
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
            var user = new User(registerUser.Email, registerUser.Location, registerUser.Web, registerUser.Bio);
            await _userDbContext.AddAsync(user);
            await _userDbContext.SaveChangesAsync();
        }

        public async void EditBio(Guid userId, String bio)
        {
            var user = _userDbContext.Users.Single(u => u.UserId == userId);
            _userDbContext.Entry(user.Bio).CurrentValues.SetValues(bio);
            await _userDbContext.SaveChangesAsync();
        }
    }
}
