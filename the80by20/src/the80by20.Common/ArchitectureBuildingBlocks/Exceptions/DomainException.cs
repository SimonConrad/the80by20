namespace the80by20.Common.ArchitectureBuildingBlocks.Exceptions;

public class DomainException : CustomException
{
    public DomainException(string msg) : base(msg)
    {
    }
}