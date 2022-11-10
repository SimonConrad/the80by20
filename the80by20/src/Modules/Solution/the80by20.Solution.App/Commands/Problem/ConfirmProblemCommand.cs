using MediatR;
using the80by20.Modules.Solution.Domain.Problem;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.Commands.Problem;

// TODO
// Clean-up attributes duplicates - e.x. to attributes for same concepts commandddd and [CommandCqrs] leave second one

// TODO
// implement cqrs in module solution like in users module,
// keep only mediatr commented solution or in markdown wiki with mediatr setup + decorators, after remove from code
[CommandDdd]
public sealed record ConfirmProblemCommand(ProblemId ProblemId) : IRequest<ProblemId>;