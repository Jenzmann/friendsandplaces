namespace FriendsAndPlaces.Models.API
{
    public class LoginModel
    {
        public string LoginName { get; set; }
        public PasswordModel Passwort { get; set; }
    }

    public class PasswordModel
    {
        public string Passwort { get; set; }
    }
}
