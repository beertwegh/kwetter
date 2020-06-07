using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AuthService.Helpers;
using Microsoft.Net.Http.Headers;

namespace AuthService.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthUser authUser)
        {
            var token = await _authService.Authenticate(authUser);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        [HttpGet]
        [Authorize]
        public async Task<int> CurrentUserId()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var userId = _authService.GetClaim(accessToken, Globals.UserIdClaim);
            return int.Parse(userId);
        }
    }
}