namespace the80by20.Domain.ArchitectureBuildingBlocks;

public abstract class CustomException : Exception
{
    protected CustomException(string message) : base(message)
    {
    }
}