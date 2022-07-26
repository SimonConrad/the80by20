namespace Common.DDD;

public sealed class InvalidEntityIdException : Exception
{
    public InvalidEntityIdException(object id) : base($"Cannot set: {id}  as entity identifier.")
    {
    }
}