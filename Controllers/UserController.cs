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
        /// <returns>Returns a list of all users if the authentication is valid.</returns>
        /// <response code="200">Returns the list of users.</response>
        /// <response code="400">If the authentication is invalid.</response>
        [HttpGet("getBenutzer")]
        public async Task<IActionResult> GetUser([FromQuery] string login, [FromQuery] string session)
        {
            if (!_authTokenService.ValidateAuth(login, session))
            {
                return BadRequest();
            }

            var users = _userService.GetAllUsers();

            return Ok(users);
        }

        //service7
        /// <summary>
        /// Retrieves the location of a user.
        /// </summary>
        /// <param name="login">The login name for authentication.</param>
        /// <param name="session">The session token for authentication.</param>
        /// <param name="id">The ID of the user.</param>
        /// <returns>Returns the coordinates of the user if online and authenticated.  If the user is offline, it should return the home address. Currently, it returns Ok().</returns>
        /// <response code="200">Returns the coordinates of the user.</response>
        /// <response code="400">If the authentication is invalid.</response>
        [HttpGet("getStandort")]
        public async Task<IActionResult> GetLocation([FromQuery] string login, [FromQuery] string session, [FromQuery] string id)
        {
            if (!_authTokenService.ValidateAuth(login, session))
            {
                return BadRequest();
            }

            var user = _userService.GetUser(id);
            var isOnline = _authTokenService.HasAuth(id);
            if (isOnline)
            {
                return Ok(new CoordinateModel()
                {
                    Breitengrad = user.Breitengrad ?? 0,
                    Laengengrad = user.Laengengrad ?? 0,
                });
            }

            //TODO return Home Adress
            return Ok();
        }

        //service6
        /// <summary>
        /// Sets the location coordinates for a user.
        /// </summary>
        /// <param name="model">The location data containing the login name, session token, and coordinates.</param>
        /// <returns>Returns Ok if the location is successfully updated.</returns>
        /// <response code="200">Location updated successfully.</response>
        /// <response code="400">If the authentication is invalid.</response>
        [HttpPut("setStandort")]
        public async Task<IActionResult> SetLocation([FromBody] LocationModel model)
        {
            if (!_authTokenService.ValidateAuth(model.LoginName, model.Sitzung))
            {
                return BadRequest();
            }

            var userModel = _userService.GetUser(model.LoginName);
            userModel.Breitengrad = model.Standort.Breitengrad;
            userModel.Laengengrad = model.Standort.Laengengrad;

            return Ok();
        }
    }
}
