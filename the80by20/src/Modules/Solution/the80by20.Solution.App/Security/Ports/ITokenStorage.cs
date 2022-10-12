namespace the80by20.Solution.App.Security.Ports;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}