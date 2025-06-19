namespace FriendsAndPlaces.Models.API;

public class GeoapifyAdressModel
{
    public List<GeoapifyAdressFeatureModel> Features { get; set; }
}

public class GeoapifyAdressFeatureModel
{
    public GeoapifyAdressFGeometryModel Geometry { get; set; }
}

public class GeoapifyAdressFGeometryModel
{
    public List<float> Coordinates { get; set; }
}