using FriendsAndPlaces.Models.API;
using FriendsAndPlaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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
        //Request Ready
        //Implementation Ready
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
        //Request Ready
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
        //Request Ready
        //Implementation Ready
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
