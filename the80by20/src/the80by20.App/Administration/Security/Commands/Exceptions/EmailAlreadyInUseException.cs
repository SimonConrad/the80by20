using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.App.Administration.Security.Commands.Exceptions;

public sealed class EmailAlreadyInUseException : CustomException
{
    public string Email { get; }

    public EmailAlreadyInUseException(string email) : base($"Email: '{email}' is already in use.")
    {
        Email = email;
    }
}