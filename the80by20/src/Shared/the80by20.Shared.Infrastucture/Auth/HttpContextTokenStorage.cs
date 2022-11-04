using Microsoft.AspNetCore.Http;
using the80by20.Shared.Abstractions.Auth;

namespace the80by20.Shared.Infrastucture.Auth
{
    internal sealed class HttpContextTokenStorage : ITokenStorage
    {
        private const string TokenKey = "jwt";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Set(JsonWebToken jwt) => _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, jwt);

        public JsonWebToken Get()
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                return null;
            }

            if (_httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt))
            {
                return jwt as JsonWebToken;
            }

            return null;
        }
    }
}
