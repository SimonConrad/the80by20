using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Solution.Domain.Solution;

[DomainExceptionDdd]
public class SolutionToProblemException : The80by20Exception
{
    public SolutionToProblemException(string msg) : base(msg)
    {
    }
}