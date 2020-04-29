using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.Models;

namespace ProfileService.Services
{
    public interface 
        IProfileService
    {
        Task<Profile> GetProfileByUserId(Guid userId);
    }
}
