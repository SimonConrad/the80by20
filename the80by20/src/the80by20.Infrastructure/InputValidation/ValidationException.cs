using the80by20.Common.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Infrastructure.InputValidation;

public class InputValidationException : CustomException
{
    public InputValidationException(string message) : base(message)
    {
    }
}