using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Solution.Domain.Capabilities;
using the80by20.Solution.Domain.Operations.Solution;

namespace the80by20.Solution.App.Commands.Solution;

[CommandDdd]
public sealed record RemoveSolutionElementCommand(SolutionToProblemId SolutionToProblemId, SolutionElement SolutionElement)
    : IRequest<SolutionToProblemId>;