using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.App.Administration.Security.Commands.Exceptions;

public sealed class UsernameAlreadyInUseException : CustomException
{
    public string Username { get; }

    public UsernameAlreadyInUseException(string username) : base($"Username: '{username}' is already in use.")
    {
        Username = username;
    }
}