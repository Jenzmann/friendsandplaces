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
        public int Breitengrad { get; set; }
        public int Laengengrad { get; set; }
    }
}
