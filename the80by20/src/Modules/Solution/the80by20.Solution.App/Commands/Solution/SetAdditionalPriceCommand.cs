using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel.Capabilities;
using the80by20.Solution.Domain.Operations.Solution;

namespace the80by20.Solution.App.Commands.Solution;

[CommandDdd]
public sealed record SetAdditionalPriceCommand(SolutionToProblemId SolutionToProblemId, Money AdditionalPrice)
    : IRequest<SolutionToProblemId>;