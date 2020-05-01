using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
    }
}