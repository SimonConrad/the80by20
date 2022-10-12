namespace the80by20.Users.App.Ports;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}