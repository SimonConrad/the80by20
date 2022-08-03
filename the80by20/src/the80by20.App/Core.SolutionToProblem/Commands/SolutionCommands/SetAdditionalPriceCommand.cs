using MediatR;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Domain.SharedKernel.Capabilities;

namespace the80by20.App.Core.SolutionToProblem.Commands.SolutionCommands;

[CommandDdd]
public sealed record SetAdditionalPriceCommand(SolutionToProblemId SolutionToProblemId, Money AdditionalPrice) 
    : IRequest<SolutionToProblemId>;