namespace FriendsAndPlaces.Models.API
{
    public class LocationModel
    {
        public string LoginName { get; set; }
        public string Sitzung { get; set; }

        public CoordinateModel Standort { get; set; }
    }

    public class CoordinateModel
    {
        public float? Breitengrad { get; set; }
        public float? Laengengrad { get; set; }
    }
}
