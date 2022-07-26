namespace Common.DDD;

public class DomainException : Exception
{
    public DomainException(string msg) : base(msg)
    {
    }
}