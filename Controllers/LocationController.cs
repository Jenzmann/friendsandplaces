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

        //todo
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
            return Ok(await _locationService.GetCityFromPostalCode(postalcode).ConfigureAwait(false));
        }
        
        //todo
        //service9
        /// <summary>
        /// Retrieves geographical coordinates based on the provided address details.
        /// </summary>
        /// <param name="land">The country code.</param>
        /// <param name="plz">The postal code.</param>
        /// <param name="ort">The city or town name.</param>
        /// <param name="strasse">The street address.</param>
        /// <returns>Returns an Ok result with the coordinates for the specified address.</returns>
        /// <response code="200">Always returns 200 OK with coordinate information (latitude and longitude).</response>
        [HttpGet("getStandortPerAdresse")]
        public async Task<IActionResult> GetLocationFromAddress([FromQuery] string land, [FromQuery] string plz, [FromQuery] string ort, [FromQuery] string strasse)
        {
            var coordinates = await _locationService.GetLocationFromAddress(land, plz, ort, strasse).ConfigureAwait(false);
            return Ok(coordinates);
        }
    }
}
