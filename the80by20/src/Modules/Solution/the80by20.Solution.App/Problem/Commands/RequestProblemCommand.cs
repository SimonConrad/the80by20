﻿using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Commands;

namespace the80by20.Modules.Solution.App.Problem.Commands;


[CommandCqrs]
public sealed record RequestProblemCommand(
    Guid Id,
    string Description,
    Guid Category,
    Guid UserId,
    SolutionType[] SolutionElementTypes) : ICommand;
