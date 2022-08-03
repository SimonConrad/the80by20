﻿using MediatR;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.App.Core.SolutionToProblem.Commands.SolutionCommands;

[CommandDdd]
public sealed record FinishSolutionCommand(SolutionToProblemId SolutionToProblemId) 
    : IRequest<SolutionToProblemId>;
