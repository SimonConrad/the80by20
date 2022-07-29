using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.App.Administration.Security.User.Exceptions;

public sealed class InvalidPasswordException : CustomException
{
    public InvalidPasswordException() : base("Invalid password.")
    {
    }
}