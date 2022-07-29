namespace the80by20.Domain.ArchitectureBuildingBlocks;

public sealed class InvalidEntityIdException : CustomException
{
    public InvalidEntityIdException(object id) : base($"Cannot set: {id}  as entity identifier.")
    {
    }
}