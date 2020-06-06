using ProfileService.Models;
using ProfileService.Repository;
using System;
using System.Threading.Tasks;
using ProfileService.Helpers.MessageBroker;

namespace ProfileService.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ISendMessageBroker _sendMessageBroker;

        public ProfileService(IProfileRepository profileRepository, ISendMessageBroker sendMessageBroker)
        {
            _profileRepository = profileRepository;
            _sendMessageBroker = sendMessageBroker;
        }

        public async Task<Profile> GetProfileByUserId(Guid userId)
        {
            var profile = await _profileRepository.GetProfileByUserId(userId);
            return profile;
        }

        public void UserRegistered(UserRegistrationModel model)
        {
            var profile = new Profile
            {
                UserId = model.UserId,
                ProfileName = model.ProfileName
            };
            _profileRepository.SaveNewProfile(profile);
        }

        public string GetUserName(Guid guid)
        {
            return _profileRepository.GetUserName(guid);
        }

        public void EditProfileName(string newName, Guid userId)
        {
            _profileRepository.EditProfileName(newName, userId);
            _sendMessageBroker.EditProfileName(new EditProfileName { NewName = newName, UserId = userId });
        }
    }
}
