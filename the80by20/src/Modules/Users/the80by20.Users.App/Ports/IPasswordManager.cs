namespace the80by20.Users.App.Ports;

public interface IPasswordManager
{
    string Secure(string password);
    bool Validate(string password, string securedPassword);
}