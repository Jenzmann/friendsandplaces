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
        /// <summary>
        /// Retrieves the city name for a given postal code.
        /// </summary>
        /// <param name="postalcode">The postal code to look up.</param>
        /// <param name="username">The username for authentication (friendsandplaces).</param>
        /// <returns>Returns the city name associated with the postal code.</returns>
        /// <response code="200">Returns the city name.</response>
        /// <response code="400">If the postal code is invalid or not found.</response>
        /// <response code="401">If the username is incorrect.</response>
        [HttpGet("getOrt")]
        public async Task<IActionResult> GetLocationFromPostalCode([FromQuery] string postalcode, [FromQuery] string username)
        {
            
            return Ok(await _locationService.GetCityFromPostalCode(postalcode));
        }

        //service9
        /// <summary>
        /// Retrieves location information based on the provided address details.
        /// </summary>
        /// <param name="land">The country.</param>
        /// <param name="plz">The postal code.</param>
        /// <param name="ort">The city.</param>
        /// <param name="strasse">The street address.</param>
        /// <returns>Returns Ok if the location is found.</returns>
        /// <response code="200">Location found successfully.</response>
        /// <response code="400">If the address is invalid or not found.</response>
        [HttpGet("getStandortPerAdresse")]
        public async Task<IActionResult> GetLocationFromAddress([FromQuery] string land, [FromQuery] string plz, [FromQuery] string ort, [FromQuery] string strasse)
        {
            return Ok();
        }
    }
}
