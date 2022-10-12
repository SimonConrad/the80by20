namespace the80by20.Solution.App.Security.Ports;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role, string userName);
}