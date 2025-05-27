using FriendsAndPlaces.Models.Internal;
using FriendsAndPlaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FriendsAndPlaces.Controllers
{
    public class LocationController : Controller
    {
        private readonly LocationService _locationService;

        
        public LocationController(LocationService locationService)
        {
            _locationService = locationService;
        }

        //service3
        //Request Ready
        //Implementation Ready
        // (username: friendsandplaces , password: Dvelop1!)
        [HttpGet("getOrt")]
        public async Task<IActionResult> GetLocationFromPostalCode([FromQuery] string postalcode, [FromQuery] string username)
        {
            
            return Ok(await _locationService.GetCityFromPostalCode(postalcode));
        }

        //service9
        //Request Ready
        [HttpGet("getStandortPerAdresse")]
        public async Task<IActionResult> GetLocationFromAddress([FromQuery] string land, [FromQuery] string plz, [FromQuery] string ort, [FromQuery] string strasse)
        {
            return Ok();
        }
    }
}
