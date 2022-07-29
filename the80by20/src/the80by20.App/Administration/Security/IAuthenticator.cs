namespace the80by20.App.Administration.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}