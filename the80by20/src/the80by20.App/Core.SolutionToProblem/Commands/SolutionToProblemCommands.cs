﻿using MediatR;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Capabilities;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Domain.SharedKernel.Capabilities;

namespace the80by20.App.Core.SolutionToProblem.Commands;

[CommandDdd]
public sealed record StartWorkingOnSolutionCommand(ProblemId ProblemId) 
    : IRequest<SolutionToProblemId>;

[CommandDdd]
public sealed record SetBasePriceOfSolutionCommand(SolutionToProblemId SolutionToProblemId) 
    : IRequest<SolutionToProblemId>;

[CommandDdd]
public sealed record AddSolutionElementCommand(SolutionToProblemId SolutionToProblemId, SolutionElement SolutionElement) 
    : IRequest<SolutionToProblemId>;

[CommandDdd]
public sealed record RemoveSolutionElementCommand(SolutionToProblemId SolutionToProblemId, SolutionElement SolutionElement) 
    : IRequest<SolutionToProblemId>;

[CommandDdd]
public sealed record SetSolutionSummaryCommand(SolutionToProblemId SolutionToProblemId, SolutionSummary SolutionSummary) 
    : IRequest<SolutionToProblemId>;

// todo handler
[CommandDdd]
public sealed record SetAdditionalPriceCommand(SolutionToProblemId SolutionToProblemId, Money AdditionalPrice) 
    : IRequest<SolutionToProblemId>;

// todo handler
[CommandDdd]
public sealed record FinishSolutionCommand(SolutionToProblemId SolutionToProblemId) 
    : IRequest<SolutionToProblemId>;