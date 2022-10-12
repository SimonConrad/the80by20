namespace the80by20.Users.App.Ports;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role, string userName);
}