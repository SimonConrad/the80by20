using the80by20.Common.ArchitectureBuildingBlocks.Exceptions;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

[DomainExceptionDdd]
public class SolutionToProblemException : CustomException
{
    public SolutionToProblemException(string msg) : base(msg)
    {
    }
}