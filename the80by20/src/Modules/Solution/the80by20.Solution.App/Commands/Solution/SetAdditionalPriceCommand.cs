using MediatR;
using the80by20.Modules.Solution.Domain.Operations.Solution;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.SharedKernel.Capabilities;

namespace the80by20.Modules.Solution.App.Commands.Solution;

[CommandDdd]
public sealed record SetAdditionalPriceCommand(SolutionToProblemId SolutionToProblemId, Money AdditionalPrice)
    : IRequest<SolutionToProblemId>;