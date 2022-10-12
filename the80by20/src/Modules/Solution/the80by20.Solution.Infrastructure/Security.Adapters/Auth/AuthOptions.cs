namespace the80by20.Infrastructure.Security.Adapters.Auth;

public sealed class AuthOptions
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string SigningKey { get; set; }
    public TimeSpan? Expiry { get; set; }
}