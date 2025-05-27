namespace FriendsAndPlaces.Models.Internal
{
    public class GeoNamesPostalCodeListModel
    {
        public List<CityModel> PostalCodes { get; set; }
    }

    public class CityModel
    {
        public string CountryCode { get; set; }
        public string PlaceName { get; set; }
    }
}
