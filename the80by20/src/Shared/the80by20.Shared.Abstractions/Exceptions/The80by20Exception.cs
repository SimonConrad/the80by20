namespace the80by20.Shared.Abstractions.Exceptions;

public abstract class The80by20Exception : Exception
{
    protected The80by20Exception(string message) : base(message)
    {
    }
}