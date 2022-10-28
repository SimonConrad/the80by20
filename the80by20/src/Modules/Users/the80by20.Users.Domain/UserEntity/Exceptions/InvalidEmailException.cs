using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Users.Domain.UserEntity.Exceptions;

public sealed class InvalidEmailException : The80by20Exception
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
        Email = email;
    }
}