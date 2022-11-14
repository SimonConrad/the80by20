using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Commands;

namespace the80by20.Modules.Solution.App.Problem.Commands;



// TODO
// Clean-up attributes duplicates - e.x. to attributes for same concepts commandddd and [CommandCqrs] leave second one

// TODO
// implement cqrs in module solution like in users module,
// keep only mediatr commented solution or in markdown wiki with mediatr setup + decorators, after remove from code

[CommandCqrs]
public sealed record RequestProblemCommand(
    Guid Id,
    string Description,
    Guid Category,
    Guid UserId,
    SolutionType[] SolutionElementTypes) : ICommand;
