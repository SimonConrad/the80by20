using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;

namespace the80by20.Solution.Infrastructure.InputValidation
{
    public class InputValidationException : CustomException
    {
        public InputValidationException(string message) : base(message)
        {
        }
    }
}