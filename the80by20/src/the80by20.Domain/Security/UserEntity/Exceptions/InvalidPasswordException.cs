using the80by20.Common.ArchitectureBuildingBlocks;
using the80by20.Common.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Domain.Security.UserEntity.Exceptions;

public sealed class InvalidPasswordException : CustomException
{
    public InvalidPasswordException() : base("Invalid password.")
    {
    }
}