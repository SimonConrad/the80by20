using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Users.App.Commands.Exceptions;

public class InvalidCredentialsException : The80by20Exception
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}