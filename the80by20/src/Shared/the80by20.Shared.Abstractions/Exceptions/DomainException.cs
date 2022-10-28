namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;

public class DomainException : The80by20Exception
{
    public DomainException(string msg) : base(msg)
    {
    }
}