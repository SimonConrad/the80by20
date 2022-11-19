using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.Commands;

namespace the80by20.Modules.Solution.App.Problem.Commands;

public sealed record UpdateProblemCommand(Guid ProblemId,
    string Description,
    Guid Category,
    SolutionType[] SolutionTypes,
    UpdateDataScope UpdateScope) : ICommand;

public enum UpdateDataScope
{
    All,
    OnlyData,
    OnlySolutionTypes
}