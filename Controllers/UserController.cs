using FriendsAndPlaces.Models.API;
using FriendsAndPlaces.Services;
using Microsoft.AspNetCore.Mvc; 

namespace FriendsAndPlaces.Controllers
{
    public class UserController : Controller
    {
        private readonly AuthTokenService _authTokenService;
        private readonly UserService _userService;
        
        public UserController(AuthTokenService authTokenService, UserService userService)
        {
            _authTokenService = authTokenService;
            _userService = userService;
        }

        //service8
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <param name="login">The login name for authentication.</param>
        /// <param name="session">The session token for authentication.</param>
        /// <returns>Returns an Ok result with either a list of all users or an empty object.</returns>
        /// <response code="200">Always returns 200 OK. Returns a list of users if authentication is valid, otherwise returns an empty JSON object.</response>
        [HttpGet("getBenutzer")]
        public IActionResult GetUser([FromQuery] string login, [FromQuery] string session)
        {
            if (!_authTokenService.ValidateAuth(login, session))
            {
                return Ok(new { });
            }

            var users = _userService.GetAllUsers();

            return Ok(users);
        }

        //service7
        /// <summary>
        /// Retrieves the location coordinates of a specified user.
        /// </summary>
        /// <param name="login">The login name of the authenticated user making the request.</param>
        /// <param name="session">The session token of the authenticated user.</param>
        /// <param name="id">The login name of the user whose location is being requested.</param>
        /// <returns>Returns an Ok result with either the user's coordinates or an empty object if authentication fails.</returns>
        /// <response code="200">Always returns 200 OK. Returns the coordinates if authentication is valid and the requested user is online, otherwise returns an empty JSON object.</response>
        [HttpGet("getStandort")]
        public IActionResult GetLocation([FromQuery] string login, [FromQuery] string session, [FromQuery] string id)
        {
            if (!_authTokenService.ValidateAuth(login, session) || !_authTokenService.HasAuth(id))
            {
                return Ok(new {});
            }
            
            var cordination = _userService.GetLocation(id);
            return Ok(cordination);
        }

        //service6
        /// <summary>
        /// Sets the location coordinates for a user.
        /// </summary>
        /// <param name="model">The location data containing the login name, session token, and coordinates.</param>
        /// <returns>Returns an Ok result with a JSON response indicating whether the location update was successful.</returns>
        /// <response code="200">Always returns 200 OK. The response body contains a JSON object with 'ergebnis' set to true if the location was successfully updated, or false if the authentication failed or the update was unsuccessful.</response>
        [HttpPut("setStandort")]
        public IActionResult SetLocation([FromBody] LocationModel model)
        {
            if (!_authTokenService.ValidateAuth(model.LoginName, model.Sitzung))
            {
                return Ok(new { ergebnis = false });
            }
            
            var result = _userService.UpdateLocation(model.LoginName, model);
            return Ok(new { ergebnis = result });
        }
    }
}
