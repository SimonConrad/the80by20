namespace the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;

public abstract class CustomException : Exception
{
    protected CustomException(string message) : base(message)
    {
    }
}