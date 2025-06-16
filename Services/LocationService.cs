using FriendsAndPlaces.Models.API;
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
        
        public async Task<CoordinateModel> GetLocationFromAddress(string country, string postalCode, string city, string street)
        {
            //todo
            // var response = await _httpClient.GetFromJsonAsync<GeoNamesAddressModel>($"http://api.geonames.org/searchJSON?country={country}&postalcode={postalCode}&city={city}&street={street}&username=friendsandplaces");
            // if (response == null || !response.Geonames.Any())
            // {
            //     throw new Exception("GetLocationFromAddress failed");
            // }
            //
            // var location = response.Geonames.FirstOrDefault();
            return new CoordinateModel
            {
                Breitengrad = 0,
                Laengengrad = 0
            };
        }
    }
}
