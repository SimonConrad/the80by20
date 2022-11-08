using MediatR;
using the80by20.Modules.Solution.Domain.Solution;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Solution.App.Commands.Solution;

[CommandDdd]
public sealed record SetBasePriceOfSolutionCommand(SolutionToProblemId SolutionToProblemId)
    : IRequest<SolutionToProblemId>;