using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Commands;

namespace the80by20.Modules.Solution.App.Commands.Problem;

[CommandCqrs]
public sealed record CreateProblemCommand(
    Guid Id,
    string Description,
    Guid Category,
    Guid UserId,
    SolutionType[] SolutionElementTypes) : ICommand;
