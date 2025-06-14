using FriendsAndPlaces.Models.API;
using FriendsAndPlaces.Models.Internal;

namespace FriendsAndPlaces.Services
{
    public class UserService
    {
        private List<UserFullModel> _users;

        public UserService()
        {
            _users = new List<UserFullModel>();
        }

        public void AddUser(UserModel user)
        {
            _users.Add(new UserFullModel(user));
        }

        public void RemoveUser(string loginName)
        {
            _users.RemoveAll(x => x.LoginName == loginName);
        }

        public UserFullModel? GetUser(string loginName)
        {
            return _users.FirstOrDefault(x => x.LoginName == loginName);
        }

        public List<UserLiteModel> GetAllUsers()
        {
            return _users.Select(userFullModel => new UserLiteModel { LoginName = userFullModel.LoginName, NachName = userFullModel.NachName, Vorname = userFullModel.Vorname, }).ToList();
        }

        public bool CheckLoginData(string loginName, string password)
        {
            return _users.Any(x => x.LoginName == loginName && x.Passwort.Passwort == password);
        }

        public bool LoginNameExists(string loginName)
        {
            return _users.Any(x => x.LoginName == loginName);
        }
    }
}
