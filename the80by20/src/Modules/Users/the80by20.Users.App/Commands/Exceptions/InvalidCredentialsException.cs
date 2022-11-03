using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Users.App.Commands.Exceptions;

public class InvalidCredentialsException : The80by20Exception
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}