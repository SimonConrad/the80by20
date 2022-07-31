using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.App.Administration.Security.Commands.Exceptions;

public class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}