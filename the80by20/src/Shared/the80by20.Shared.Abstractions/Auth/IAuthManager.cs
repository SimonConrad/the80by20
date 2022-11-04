namespace the80by20.Shared.Abstractions.Auth;

public interface IAuthManager
{
    JsonWebToken CreateToken(string userId, string role = null, string audience = null,
            IDictionary<string, IEnumerable<string>> claims = null, string email = null);
}