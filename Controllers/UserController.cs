using FriendsAndPlaces.Models;
using Microsoft.AspNetCore.Mvc;

namespace FriendsAndPlaces.Controllers
{
    public class UserController : Controller
    {
        //service8
        [HttpGet("getBenutzer")]
        public async Task<IActionResult> GetUser()
        {
            return Ok();
        }

        //service7
        [HttpGet("getStandort")]
        public async Task<IActionResult> GetLocation()
        {
            return Ok();
        }

        //service6
        [HttpPut("setStandort")]
        public async Task<IActionResult> SetLocation([FromBody] LocationModel model)
        {
            return Ok();
        }
    }
}
