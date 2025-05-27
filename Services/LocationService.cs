using FriendsAndPlaces.Models.Internal;

namespace FriendsAndPlaces.Services
{
    public class LocationService
    {
        private readonly HttpClient _httpClient;

        public LocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetCityFromPostalCode(string postalCode)
        {
            var cities = await _httpClient.GetFromJsonAsync<GeoNamesPostalCodeListModel>($"http://api.geonames.org/postalCodeSearchJSON?postalcode={postalCode}&username=friendsandplaces");
            if (!cities.PostalCodes.Any(x => x.CountryCode == "DE"))
            {
                throw new Exception("GetCityFromPostalCode failed");
            }

            var city = cities.PostalCodes.FirstOrDefault(x => x.CountryCode == "DE").PlaceName;
            return city;
        }
    }
}
