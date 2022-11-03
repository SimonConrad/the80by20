using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Users.Domain.UserEntity.Exceptions;

public sealed class InvalidPasswordException : The80by20Exception
{
    public InvalidPasswordException() : base("Invalid password.")
    {
    }
}