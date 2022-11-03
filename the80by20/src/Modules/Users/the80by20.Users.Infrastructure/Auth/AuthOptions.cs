namespace the80by20.Modules.Users.Infrastructure.Auth
{
    public sealed class AuthOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SigningKey { get; set; }
        public TimeSpan? Expiry { get; set; }
    }
}