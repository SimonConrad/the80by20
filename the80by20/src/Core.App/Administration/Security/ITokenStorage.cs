namespace Core.App.Administration.Security;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}