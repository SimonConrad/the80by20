using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Users.App.Commands.Exceptions;

public sealed class EmailAlreadyInUseException : The80by20Exception
{
    public string Email { get; }

    public EmailAlreadyInUseException(string email) : base($"Email: '{email}' is already in use.")
    {
        Email = email;
    }
}