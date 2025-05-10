using Microsoft.AspNetCore.Mvc;

namespace FriendsAndPlaces.Controllers
{
    public class LocationController : Controller
    {
        //service3
        [HttpGet("getOrt")]
        public async Task<IActionResult> GetLocationFromPostalCode([FromQuery] string postalcode, [FromQuery] string username)
        {
            return Ok();
        }

        //service9
        [HttpGet("getStandortPerAdresse")]
        public async Task<IActionResult> GetLocationFromAddress()
        {
            return Ok();
        }
    }
}
