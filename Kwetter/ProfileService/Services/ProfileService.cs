using ProfileService.Models;
using ProfileService.Repository;
using System;
using System.Threading.Tasks;

namespace ProfileService.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
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
    }
}
