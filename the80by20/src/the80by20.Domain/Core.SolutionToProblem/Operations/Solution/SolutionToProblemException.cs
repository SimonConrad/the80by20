using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

[DomainExceptionDdd]
public class SolutionToProblemException : CustomException
{
    public SolutionToProblemException(string msg) : base(msg)
    {
    }
}