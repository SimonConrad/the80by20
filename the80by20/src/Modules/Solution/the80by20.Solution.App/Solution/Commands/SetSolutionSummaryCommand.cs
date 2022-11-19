using the80by20.Modules.Solution.Domain.Solution.Entities;
using the80by20.Modules.Solution.Domain.Solution.ValueObjects;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.Solution.Commands;

// //info if we want to send 3 commands at once from ui (example: AddSolutionElementCommand, SetSolutionSummaryCommand, SetAdditionalPriceCommand)
// //then such http web api post endpoint can be expposed wiitch transfer above coammnd data in its payload, then application service will handle it properly


public sealed record SetSolutionSummaryCommand(SolutionToProblemId SolutionToProblemId, SolutionSummary SolutionSummary)
: ICommand;