using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.App.Administration.Security.User.Exceptions;

public sealed class InvalidEmailException : CustomException
{
    public string Email { get; }

    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
        Email = email;
    }
}