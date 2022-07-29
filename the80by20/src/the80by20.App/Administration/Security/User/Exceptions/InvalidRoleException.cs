using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.App.Administration.Security.User.Exceptions;

public sealed class InvalidRoleException : CustomException
{
    public string Role { get; }

    public InvalidRoleException(string role) : base($"Role: '{role}' is invalid.")
    {
        Role = role;
    }
}