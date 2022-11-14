using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.Solution.Commands;

public sealed record FinishSolutionCommand(SolutionToProblemId SolutionToProblemId)
    : ICommand;

