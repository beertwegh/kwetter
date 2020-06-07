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
        public async Task<IActionResult> Registration([FromBody] UserRegistrationModel model)
        {
            _userService.Registration(model);
            return Ok();
        }

        [HttpPost("EditUser")]
        [GetCurrentUser]
        public async Task<IActionResult> EditUser([FromBody] User user)
        {
            user.UserId = GetCurrentUser.UserId;
            _userService.EditUser(user);
            return Ok();
        }

        [HttpGet("details")]
        [GetCurrentUser]
        public async Task<IActionResult> GetUserDetails()
        {
            var user = await _userService.GetUserDetails(GetCurrentUser.UserId);
            return Ok(user);
        }

        [HttpDelete("delete")]
        [GetCurrentUser]
        public async Task<IActionResult> DeleteUser()
        {
            if (GetCurrentUser.UserId == Guid.Empty)
                return Unauthorized();
            _userService.DeleteUser(GetCurrentUser.UserId);
            return Ok();
        }
    }

}