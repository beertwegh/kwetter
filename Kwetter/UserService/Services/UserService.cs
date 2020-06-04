using System;
using System.Threading.Tasks;
using MessageService.Helpers.MessageBroker;
using MessageService.Models;
using MessageService.Repository.Interface;

namespace MessageService.Services
{
    public class UserService : IUserService
    {
        private readonly ISendMessageBroker _messageBroker;
        private readonly IUserRepository _userRepository;
        public UserService(ISendMessageBroker messageBroker, IUserRepository userRepository)
        {
            _messageBroker = messageBroker;
            _userRepository = userRepository;
        }

        public async void Registration(UserRegistrationModel model)
        {
            model.UserId = Guid.NewGuid();
            _messageBroker.Registration(model);
            _userRepository.Register(model);

        }

        public async void EditUser(User user)
        {
            _userRepository.EditUser(user);
        }

        public async Task<User> GetUserDetails(Guid userId)
        {
            return await _userRepository.GetUserDetails(userId);
        }
    }
}
