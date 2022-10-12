﻿using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Solution.App.SolutionToProblem.Commands.SolutionCommands;

[CommandDdd]
public sealed record StartWorkingOnSolutionCommand(ProblemId ProblemId)
    : IRequest<SolutionToProblemId>;