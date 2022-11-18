using System.Collections.Generic;
using the80by20.Shared.Infrastucture.Auth;
using the80by20.Shared.Infrastucture.Time;

namespace the80by20.Tests.Shared
{
    public static class AuthHelper
    {
        private static readonly AuthManager AuthManager;

        static AuthHelper()
        {
            var options = OptionsHelper.GetOptions<AuthOptions>("auth");
            AuthManager = new AuthManager(options, new Clock());
        }

        public static string GenerateJwt(string userId, string role = null, string audience = null,
            IDictionary<string, IEnumerable<string>> claims = null, string email = null)
            => AuthManager.CreateToken(userId, role, audience, claims, email: email).AccessToken; // todo eliminate email hack
    }
}