using FriendsAndPlaces.Models.API;

namespace FriendsAndPlaces.Models.Internal
{
    public class UserFullModel : UserModel
    {
        public UserFullModel(UserModel userModel)
        {
            Email = userModel.Email;
            Land = userModel.Land;
            LoginName = userModel.LoginName;
            NachName = userModel.NachName;
            Ort = userModel.Ort;
            Passwort = userModel.Passwort;
            Plz = userModel.Plz;
            Strasse = userModel.Strasse;
            Telefon = userModel.Telefon;
            Vorname = userModel.Vorname;
        }
        public float? Breitengrad { get; set; }
        public float? Laengengrad { get; set; }
    }
}
