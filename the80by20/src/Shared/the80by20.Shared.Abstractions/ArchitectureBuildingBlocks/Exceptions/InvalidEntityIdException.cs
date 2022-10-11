namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;

public sealed class InvalidEntityIdException : CustomException
{
    public InvalidEntityIdException(object id) : base($"Cannot set: {id}  as entity identifier.")
    {
    }
}