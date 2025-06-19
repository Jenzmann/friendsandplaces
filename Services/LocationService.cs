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
            var apiKey = "d15485b155c74a65908195eb49ccf398";
            
             var response = await _httpClient.GetFromJsonAsync<GeoapifyAdressModel>($"https://api.geoapify.com/v1/geocode/search?text={street}%2C%20{postalCode}%20{city}%2C%20{country}&apiKey={apiKey}");
             if (response == null || !response.Features.Any())
             {
                 throw new Exception("GetLocationFromAddress failed");
             }
            
             var location = response.Features.FirstOrDefault().Geometry.Coordinates;
            return new CoordinateModel
            {
                Breitengrad = location[0],
                Laengengrad = location[1]
            };
        }
    }
}
