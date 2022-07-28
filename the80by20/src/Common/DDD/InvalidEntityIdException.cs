namespace Common.DDD;

public sealed class InvalidEntityIdException : CustomException
{
    public InvalidEntityIdException(object id) : base($"Cannot set: {id}  as entity identifier.")
    {
    }
}