using System.Collections.Concurrent;

namespace FriendsAndPlaces.Services
{
    public class AuthTokenService
    {
        public ConcurrentDictionary<string, string> _authTokens;

        public AuthTokenService()
        {
            _authTokens = new ConcurrentDictionary<string, string>();
        }

        public string AddAuth(string loginName)
        {
            var token = Guid.NewGuid().ToString();
            if (_authTokens.TryAdd(loginName, token))
            {
                return token;
            }

            throw new Exception("AddAuth failed");
        }

        public void RemoveAuth(string loginName)
        {
            _authTokens.TryRemove(loginName, out var _);
        }

        public bool ValidateAuth(string loginName, string token)
        {
            if (!_authTokens.TryGetValue(loginName, out var realToken))
            {
                return false;
            }

            return realToken == token;
        }

        public bool HasAuth(string loginName)
        {
            if (!_authTokens.TryGetValue(loginName, out var _))
            {
                return false;
            }

            return true;
        }
    }
}
