using the80by20.Common.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Domain.Security.UserEntity.Exceptions;

public sealed class InvalidRoleException : CustomException
{
    public string Role { get; }

    public InvalidRoleException(string role) : base($"Role: '{role}' is invalid.")
    {
        Role = role;
    }
}