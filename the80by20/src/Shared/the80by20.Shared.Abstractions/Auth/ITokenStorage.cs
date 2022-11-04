namespace the80by20.Shared.Abstractions.Auth;

public interface ITokenStorage
{
    void Set(JsonWebToken jwt);
    JsonWebToken Get();
}