﻿using MediatR;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel.Capabilities;

namespace the80by20.App.Core.SolutionToProblem.Commands.SolutionCommands;

[CommandDdd]
public sealed record SetAdditionalPriceCommand(SolutionToProblemId SolutionToProblemId, Money AdditionalPrice) 
    : IRequest<SolutionToProblemId>;