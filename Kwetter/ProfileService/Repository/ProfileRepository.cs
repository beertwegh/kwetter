using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.DbContext;
using ProfileService.Models;

namespace ProfileService.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ProfileContext _profileContext;

        public ProfileRepository(ProfileContext profileContext)
        {
            _profileContext = profileContext;
        }

        public async Task<Profile> GetProfileByUserId(Guid userId)
        {
            var profile = await this._profileContext.Profiles.Where(p => p.UserId == userId).SingleOrDefaultAsync();
            return profile;
        }

        public void SaveNewProfile(Profile profile)
        {
            _profileContext.Add(profile);
            _profileContext.SaveChanges();
        }
    }
}
