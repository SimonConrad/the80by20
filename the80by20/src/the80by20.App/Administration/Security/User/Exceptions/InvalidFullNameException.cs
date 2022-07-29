using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.App.Administration.Security.User.Exceptions;

public sealed class InvalidFullNameException : CustomException
{
    public string FullName { get; }

    public InvalidFullNameException(string fullName) : base($"Full name: '{fullName}' is invalid.")
    {
        FullName = fullName;
    }
}