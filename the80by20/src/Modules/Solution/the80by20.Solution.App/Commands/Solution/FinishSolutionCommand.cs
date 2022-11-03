using MediatR;
using the80by20.Modules.Solution.Domain.Operations.Solution;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Solution.App.Commands.Solution;

[CommandDdd]
public sealed record FinishSolutionCommand(SolutionToProblemId SolutionToProblemId)
    : IRequest<SolutionToProblemId>;

