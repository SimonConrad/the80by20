using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Users.Domain.UserEntity.Exceptions;

public sealed class InvalidRoleException : The80by20Exception
{
    public string Role { get; }

    public InvalidRoleException(string role) : base($"Role: '{role}' is invalid.")
    {
        Role = role;
    }
}