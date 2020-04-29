using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.Models;
using ProfileService.Repository;

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
    }
}
