using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Solution.Domain.Operations.Solution;

namespace the80by20.Solution.App.Commands.SolutionCommands;

[CommandDdd]
public sealed record SetBasePriceOfSolutionCommand(SolutionToProblemId SolutionToProblemId)
    : IRequest<SolutionToProblemId>;