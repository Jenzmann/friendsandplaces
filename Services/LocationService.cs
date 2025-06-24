using System.Text.Json;
using FriendsAndPlaces.Models.API;
using FriendsAndPlaces.Models.Internal;
using Microsoft.AspNetCore.Mvc;

namespace FriendsAndPlaces.Services
{
    public class LocationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        
        public LocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = Environment.GetEnvironmentVariable("GEOAPIFY_API_KEY");
        }

        public async Task<HttpResponseMessage> GetCityFromPostalCode(string postalCode, string username)
        {
            var result  = await _httpClient.GetAsync($"http://api.geonames.org/postalCodeSearchJSON?postalcode={postalCode}&username={username}");
            // if (!cities.PostalCodes.Any(x => x.CountryCode == "DE"))
            // {
            //     throw new Exception("GetCityFromPostalCode failed");
            // }
            //
            // var city = cities.PostalCodes.FirstOrDefault(x => x.CountryCode == "DE").PlaceName;
            
            return result;
        }
        
        public async Task<CoordinateModel> GetLocationFromAddress(string country, string postalCode, string city, string street)
        {
             var response = await _httpClient.GetFromJsonAsync<GeoapifyAdressModel>($"https://api.geoapify.com/v1/geocode/search?text={street}%2C%20{postalCode}%20{city}%2C%20{country}&apiKey={_apiKey}");
             if (response == null || !response.Features.Any())
             {
                 throw new Exception("GetLocationFromAddress failed");
             }
            
             var location = response.Features.FirstOrDefault().Geometry.Coordinates;
            return new CoordinateModel
            {
                Breitengrad = location[1],
                Laengengrad = location[0]
            };
        }
    }
}
