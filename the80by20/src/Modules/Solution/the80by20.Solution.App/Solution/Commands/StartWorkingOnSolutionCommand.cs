using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.Solution.Commands;

[CommandDdd]
public sealed record StartWorkingOnSolutionCommand(ProblemId ProblemId)
    : IRequest<SolutionToProblemId>;