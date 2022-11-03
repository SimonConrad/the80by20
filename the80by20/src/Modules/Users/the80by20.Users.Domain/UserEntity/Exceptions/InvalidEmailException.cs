using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Users.Domain.UserEntity.Exceptions;

public sealed class InvalidEmailException : The80by20Exception
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
        Email = email;
    }
}