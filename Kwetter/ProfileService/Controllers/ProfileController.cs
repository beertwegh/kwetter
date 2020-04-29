using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Helpers;
using ProfileService.Models;
using ProfileService.Services;

namespace ProfileService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController :ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("/current")]
        [GetCurrentUser]
        public async Task<IActionResult> GetCurrentProfile()
        {
            var profile = await _profileService.GetProfileByUserId(GetCurrentUser.UserId);
            return Ok(profile);
        }

    }
}
