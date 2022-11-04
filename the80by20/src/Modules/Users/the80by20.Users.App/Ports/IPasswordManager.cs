namespace the80by20.Modules.Users.App.Ports;

public interface IPasswordManager
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string password, string securedPassword);
}