namespace the80by20.App.Security.Ports;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role, string userName);
}