using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Users.Domain.UserEntity.Exceptions;

public sealed class InvalidFullNameException : The80by20Exception
{
    public string FullName { get; }

    public InvalidFullNameException(string fullName) : base($"Full name: '{fullName}' is invalid.")
    {
        FullName = fullName;
    }
}