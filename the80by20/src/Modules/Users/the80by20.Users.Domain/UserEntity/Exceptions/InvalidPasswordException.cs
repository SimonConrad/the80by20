using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Users.Domain.UserEntity.Exceptions;

public sealed class InvalidPasswordException : The80by20Exception
{
    public InvalidPasswordException() : base("Invalid password.")
    {
    }
}