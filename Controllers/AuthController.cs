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
        /// <summary>
        /// Authenticates a user and returns an authentication token.
        /// </summary>
        /// <param name="model">The login credentials.</param>
        /// <returns>Returns an Ok result with the authentication token if login is successful; otherwise, returns a BadRequest result.</returns>
        /// <response code="200">Returns the authentication token.</response>
        /// <response code="400">If the login attempt fails.</response>
        [HttpPost("login", Name = "login")]
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
        /// <summary>
        /// Checks if a login name is already in use.
        /// </summary>
        /// <param name="id">The login name to check.</param>
        /// <returns>Returns true if the login name is not in use, otherwise false.</returns>
        /// <response code="200">Returns true if the login name is not in use.</response>
        /// <response code="400">If the login name is already in use.</response>
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
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userModel">The user data to register.</param>
        /// <returns>Returns Ok if the user was successfully registered.</returns>
        /// <response code="200">User registered successfully.</response>
        /// <response code="400">If the user data is invalid.</response>
        [HttpPost("addUser")]
        public async Task<IActionResult> Register([FromBody] UserModel userModel)
        {
            _userService.AddUser(userModel);
            return Ok();
        }
    }
}
