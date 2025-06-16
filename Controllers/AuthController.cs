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
        
        [HttpGet("/")]
        public IActionResult Root()
        {
            return Ok(new { message = "Welcome to FriendsAndPlaces API" });
        }

        //Service4
        /// <summary>
        /// Authenticates a user and returns an authentication token.
        /// </summary>
        /// <param name="model">The login credentials.</param>
        /// <returns>Returns an Ok result with either a session ID or an empty object.</returns>
        /// <response code="200">Always returns 200 OK. Returns a JSON object with 'sessionID' if login is successful, or an empty JSON object if login fails.</response>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var loginValid = _userService.CheckLoginData(model.LoginName, model.Passwort.Passwort);
            if (!loginValid)
            {
                return Ok(new { });
            }

            var token = _authTokenService.GetOrCreateToken(model.LoginName);
            return Ok(new { sessionID = token });
        }

        //service2
        /// <summary>
        /// Checks if a login name is already in use.
        /// </summary>
        /// <param name="id">The login name to check.</param>
        /// <returns>Returns an Ok result with a JSON response indicating whether the login name is available.</returns>
        /// <response code="200">Always returns 200 OK. The response body contains a JSON object with 'ergebnis' set to true if the login name is available (not in use), or false if it's already taken.</response>
        [HttpGet("checkLoginName")]
        public IActionResult CheckUserName([FromQuery] string id)
        {
            var isUsed = _userService.LoginNameExists(id);
            return Ok(new {ergebnis = !isUsed});
        }

        //service5
        /// <summary>
        /// Logs out a user by invalidating their session token.
        /// </summary>
        /// <param name="model">The logout model containing login name and session token.</param>
        /// <returns>Returns an Ok result with a JSON response indicating whether the logout was successful.</returns>
        /// <response code="200">Always returns 200 OK. The response body contains a JSON object with 'ergebnis' set to true if logout was successful, or false if the session validation failed.</response>
        [HttpPost("logout")]
        public IActionResult Logout([FromBody] LogoutModel model)
        {
            if (_authTokenService.ValidateAuth(model.LoginName, model.Sitzung))
            {
                _authTokenService.RemoveAuth(model.LoginName);
                return Ok(new {ergebnis = true});
            }

            return Ok(new {ergebnis = false});
        }

        //service1
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userModel">The user data to register.</param>
        /// <returns>Returns an Ok result with a JSON response. The JSON response indicates whether the registration was successful or provides an error message.</returns>
        /// <response code="200">Always returns 200 OK. The response body contains a JSON object with 'ergebnis' (true for success, false for failure) and 'meldung' (an error message if 'ergebnis' is false).</response>
        [HttpPost("addUser")]
        public IActionResult Register([FromBody] UserModel userModel)
        {
            if (_userService.LoginNameExists(userModel.LoginName))
            {
                return Ok(new { ergebnis = false, meldung = "LoginName bereits vorhanden" });
            }

            _userService.AddUser(userModel);
            return Ok(new { ergebnis = true, meldung = "" });
        }
    }
}
