namespace the80by20.App.Security.Ports;

public interface IPasswordManager
{
    string Secure(string password);
    bool Validate(string password, string securedPassword);
}