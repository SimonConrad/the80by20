namespace the80by20.Shared.Abstractions.Exceptions;

public sealed class InvalidEntityIdException : The80by20Exception
{
    public InvalidEntityIdException(object id) : base($"Cannot set: {id}  as entity identifier.")
    {
    }
}