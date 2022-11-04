using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Users.App.Commands.Exceptions;

internal class UserNotActiveException : The80by20Exception
{
    public Guid UserId { get; }

    public UserNotActiveException(Guid userId) : base($"User with ID: '{userId}' is not active.")
    {
        UserId = userId;
    }
}