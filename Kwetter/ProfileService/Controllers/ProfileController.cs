using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Helpers;
using ProfileService.Models;
using ProfileService.Services;

namespace ProfileService.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("current")]
        [GetCurrentUser]
        public async Task<IActionResult> GetCurrentProfile()
        {
            if (GetCurrentUser.UserId == Guid.Empty)
                return Unauthorized();
            var profile = await _profileService.GetProfileByUserId(GetCurrentUser.UserId);
            return Ok(profile);
        }

        [HttpPost("editname")]
        [GetCurrentUser]
        public async Task<IActionResult> EditProfileName([FromBody] Profile profile)
        {
            if (GetCurrentUser.UserId == Guid.Empty)
                return Unauthorized();
            var newName = profile.ProfileName;
            _profileService.EditProfileName(newName, GetCurrentUser.UserId);
            return Ok();
        }

    }
}
