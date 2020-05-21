using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProfileService.Helpers;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Registration([FromBody]UserRegistrationModel model)
        {
            _userService.Registration(model);
            return Ok();
        }

        [HttpPost("EditBio")]
        [GetCurrentUser]
        public async Task<IActionResult> EditBio([FromBody] String bio)
        {
            _userService.EditBio(GetCurrentUser.UserId, bio);
            return Ok();
        }

        [HttpGet("details")]
        [GetCurrentUser]
        public async Task<IActionResult> GetUserDetails()
        {
            var user = await _userService.GetUserDetails(GetCurrentUser.UserId);
            return Ok(user);
        }
    }
}