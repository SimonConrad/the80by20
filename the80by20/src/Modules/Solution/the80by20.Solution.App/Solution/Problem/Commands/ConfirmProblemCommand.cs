using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.Problem.Commands;

public sealed record ConfirmProblemCommand(ProblemId ProblemId) : ICommand;