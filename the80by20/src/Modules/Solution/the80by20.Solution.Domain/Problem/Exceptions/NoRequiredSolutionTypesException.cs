using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Solution.Domain.Problem.Exceptions;

public class NoRequiredSolutionTypesException : The80by20Exception
{
    public Guid ProblemId { get; }

    public NoRequiredSolutionTypesException(string msg, Guid problemId)
        : base($"{msg}, problemId: {problemId}")
        => ProblemId = problemId;
}
