namespace the80by20.Common.ArchitectureBuildingBlocks.Exceptions;

public abstract class CustomException : Exception
{
    protected CustomException(string message) : base(message)
    {
    }
}