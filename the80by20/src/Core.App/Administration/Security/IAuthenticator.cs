namespace Core.App.Administration.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}