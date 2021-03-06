﻿using System;
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

        public string GetUserName(Guid guid) {
            return _profileContext.Profiles.Where(p => p.UserId == guid).Select(p => p.ProfileName).First();
        }

        public void EditProfileName(string newName, Guid userId)
        {
            var profile =_profileContext.Profiles.Single(w => w.UserId == userId);
            profile.ProfileName = newName;
            _profileContext.SaveChanges();
        }

        public void UserDeleted(Guid userId)
        {
            var profile = _profileContext.Profiles.Single(p => p.UserId == userId);
            _profileContext.Remove(profile);
            _profileContext.SaveChanges();
        }
    }
}
