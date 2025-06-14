using FriendsAndPlaces.Models.API;
using FriendsAndPlaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FriendsAndPlaces.Controllers
{
    [ApiController]
    [Route("/")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthTokenService _authTokenService;

        public AuthController(UserService userService, AuthTokenService authTokenService)
        {
            _userService = userService;
            _authTokenService = authTokenService;
        }

        //Service4
        //Request Ready
        //Implementation Ready
        [HttpPost(Name = "login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var loginValid = _userService.CheckLoginData(model.LoginName, model.Passwort.Passwort);
            if (loginValid)
            {
                var token = _authTokenService.AddAuth(model.LoginName);
                return Ok(token);
            }
            return BadRequest();
        }

        //service2
        //Request Ready
        [HttpGet("checkLoginName")]
        public async Task<IActionResult> CheckUserName([FromQuery] string id)
        {
            var isUsed = _userService.LoginNameExists(id);
            return Ok(!isUsed);
        }

        //service 5
        //Request Ready
        //Implementation Ready
        [HttpGet("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutModel model)
        {
            if (_authTokenService.ValidateAuth(model.LoginName, model.Sitzung))
            {
                _authTokenService.RemoveAuth(model.LoginName);
                return Ok();
            }

            return BadRequest();
        }

        //service1
        //Request Ready
        //Implementation Ready
        [HttpPost("addUser")]
        public async Task<IActionResult> Register([FromBody] UserModel userModel)
        {
            _userService.AddUser(userModel);
            return Ok();
        }
    }
}
