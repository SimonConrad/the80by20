namespace the80by20.Modules.Users.App.Ports;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}