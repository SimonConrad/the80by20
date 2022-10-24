namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;

public class DomainException : CustomException
{
    public DomainException(string msg) : base(msg)
    {
    }
}