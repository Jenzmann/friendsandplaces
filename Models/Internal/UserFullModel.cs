using FriendsAndPlaces.Models.API;

namespace FriendsAndPlaces.Models.Internal
{
    public class UserFullModel : UserModel
    {
        public UserFullModel(UserModel userModel)
        {
            this.Email = userModel.Email;
            this.Land = userModel.Land;
            this.LoginName = userModel.LoginName;
            this.NachName = userModel.NachName;
            this.Ort = userModel.Ort;
            this.Passwort = userModel.Passwort;
            this.Plz = userModel.Plz;
            this.Strasse = userModel.Strasse;
            this.Telefon = userModel.Telefon;
            this.Vorname = userModel.Vorname;
        }
        public int? Breitengrad { get; set; }
        public int? Laengengrad { get; set; }
    }
}
