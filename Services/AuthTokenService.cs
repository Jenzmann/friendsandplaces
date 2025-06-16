using System.Collections.Concurrent;

namespace FriendsAndPlaces.Services
{
    public class AuthTokenService
    {
        private readonly ConcurrentDictionary<string, string> _authTokens = new ConcurrentDictionary<string, string>();

        public string GetOrCreateToken(string loginName)
        {
            _authTokens.TryGetValue(loginName, out var t);
            var token = t ?? Guid.NewGuid().ToString();
            _authTokens.TryAdd(loginName, token);
            
            return token;
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
            return _authTokens.TryGetValue(loginName, out var _);
        }
    }
}
