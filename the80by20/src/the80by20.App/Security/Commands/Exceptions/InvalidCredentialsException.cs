using the80by20.Common.ArchitectureBuildingBlocks;
using the80by20.Common.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.App.Security.Commands.Exceptions;

public class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}