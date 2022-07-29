namespace the80by20.App.Administration.Security;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}