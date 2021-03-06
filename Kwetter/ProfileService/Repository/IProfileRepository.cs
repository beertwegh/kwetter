﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.Models;

namespace ProfileService.Repository
{
    public interface IProfileRepository
    {
        Task<Profile> GetProfileByUserId(Guid userId);
        void SaveNewProfile(Profile profile);
        string GetUserName(Guid guid);
        void EditProfileName(string newName, Guid userId);
        void UserDeleted(Guid userId);
    }
}
