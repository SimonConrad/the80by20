using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Solution.Domain.Solution.Exceptions;

[DomainExceptionDdd]
public class SolutionToProblemException : The80by20Exception
{
    public Guid SolutionToProblemID { get; }

    public SolutionToProblemException(string msg, Guid solutionToProblemID) 
        : base($"{msg}, solutionID: {solutionToProblemID}")
        => SolutionToProblemID = solutionToProblemID;
}