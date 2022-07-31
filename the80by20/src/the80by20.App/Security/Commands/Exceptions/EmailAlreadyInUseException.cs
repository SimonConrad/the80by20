using the80by20.Common.ArchitectureBuildingBlocks;
using the80by20.Common.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.App.Security.Commands.Exceptions;

public sealed class EmailAlreadyInUseException : CustomException
{
    public string Email { get; }

    public EmailAlreadyInUseException(string email) : base($"Email: '{email}' is already in use.")
    {
        Email = email;
    }
}