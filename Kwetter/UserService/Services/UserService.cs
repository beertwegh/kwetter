using System;
using UserService.Helpers.MessageBroker;
using UserService.Models;
using UserService.Repository.Interface;

namespace UserService.Services
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

        public async void EditBio(Guid userId, String bio)
        {
            _userRepository.EditBio(userId, bio);
        }
    }
}
