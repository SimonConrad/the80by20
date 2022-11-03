using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Users.App.Commands.Exceptions;

public sealed class UsernameAlreadyInUseException : The80by20Exception
{
    public string Username { get; }

    public UsernameAlreadyInUseException(string username) : base($"Username: '{username}' is already in use.")
    {
        Username = username;
    }
}