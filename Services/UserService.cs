using FriendsAndPlaces.Models.API;
using FriendsAndPlaces.Models.Internal;

namespace FriendsAndPlaces.Services
{
    public class UserService
    {
        private List<UserFullModel> _users;
        private AuthTokenService _authTokenService;
        private LocationService _locationService;

        public UserService(AuthTokenService authTokenService, LocationService locationService)
        {
            _authTokenService = authTokenService;
            _locationService = locationService;
            _users = new List<UserFullModel>();
        }

        public void AddUser(UserModel user)
        {
            _users.Add(new UserFullModel(user));
        }
        
        public bool UpdateLocation(string loginName, LocationModel location)
        {
            var user = _users.FirstOrDefault(x => x.LoginName == loginName);
            if (user == null)
            {
                return false;
            }

            user.Breitengrad = location.Standort.Breitengrad;
            user.Laengengrad = location.Standort.Laengengrad;
            return true;
        }
        
        public async Task<CoordinateModel>? GetLocation(string loginName)
        {
            var user = GetUser(loginName);
            if (user == null)
            {
                return null;
            }
            if (_authTokenService.HasAuth(loginName))
            {
                if (user.Breitengrad != null && user.Laengengrad != null)
                {
                    return new CoordinateModel { Breitengrad = user.Breitengrad, Laengengrad = user.Laengengrad };
                }
            }
            
            return await _locationService.GetLocationFromAddress(user.Land, user.Plz, user.Ort, user.Strasse);
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

        public bool LoginNameValid(string loginName)
        {
            if (loginName.Length < 5)
            {
                return false;
            }
            return !_users.Any(x => x.LoginName == loginName);
        }
    }
}
