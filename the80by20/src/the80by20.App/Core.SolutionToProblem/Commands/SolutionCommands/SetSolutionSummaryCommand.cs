using MediatR;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Core.SolutionToProblem.Capabilities;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.App.Core.SolutionToProblem.Commands.SolutionCommands;

// //info if we want to send 3 commands at once from ui (example: AddSolutionElementCommand, SetSolutionSummaryCommand, SetAdditionalPriceCommand)
// //then such http web api post endpoint can be expposed wiitch transfer above coammnd data in its payload, then application service will handle it properly

[CommandDdd]
public sealed record SetSolutionSummaryCommand(SolutionToProblemId SolutionToProblemId, SolutionSummary SolutionSummary) 
    : IRequest<SolutionToProblemId>;