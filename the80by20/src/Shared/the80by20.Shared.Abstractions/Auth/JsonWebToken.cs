namespace the80by20.Shared.Abstractions.Auth;

public class JsonWebToken
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; } // TODO implement mechanism with refresh-token
    public long Expires { get; set; }
    public string Id { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public IDictionary<string, IEnumerable<string>> Claims { get; set; }
}