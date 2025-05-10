using FriendsAndPlaces.Models;
using Microsoft.AspNetCore.Mvc;

namespace FriendsAndPlaces.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        //Service4
        [HttpPost(Name = "login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            return Ok();
        }

        //service2
        [HttpGet("checkLoginName")]
        public async Task<IActionResult> CheckUserName([FromQuery] string id)
        {
            return Ok();
        }

        //service 5
        [HttpGet("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutModel model)
        {
            return Ok();
        }

        //service1
        [HttpPost("addUser")]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }
    }
}
