using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Solution.Domain.Problem.Exceptions;

[DomainExceptionDdd]
public class ProblemException : The80by20Exception
{
    public Guid ProblemId { get; }

    public ProblemException(string msg, Guid problemId)
        : base($"{msg}, problemId: {problemId}")
        => ProblemId = problemId;
}
