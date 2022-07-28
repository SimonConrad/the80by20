namespace Common.DDD;

public class DomainException : CustomException
{
    public DomainException(string msg) : base(msg)
    {
    }
}