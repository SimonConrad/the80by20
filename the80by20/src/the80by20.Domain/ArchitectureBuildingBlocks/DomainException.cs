namespace the80by20.Domain.ArchitectureBuildingBlocks;

public class DomainException : CustomException
{
    public DomainException(string msg) : base(msg)
    {
    }
}