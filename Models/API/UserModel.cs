namespace FriendsAndPlaces.Models.API
{
    public class UserModel : LoginModel
    {
        public string Vorname { get; set; }
        public string NachName { get; set; }
        public string Strasse { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Land { get; set; }
        public string Telefon { get; set; }
        public EmailModel Email { get; set; }
    }

    public class EmailModel
    {
        public string Adresse { get; set; }
    }
}
