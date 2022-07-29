using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.Domain.Core.SolutionToProblem.Operations;

[DomainExceptionDdd]
public class SolutionToProblemException : CustomException
{
    public SolutionToProblemException(string msg) : base(msg)
    {
    }
}