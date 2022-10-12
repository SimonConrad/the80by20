using MediatR;
using the80by20.Domain.Core.SolutionToProblem.Capabilities;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.App.Core.SolutionToProblem.Commands.SolutionCommands;

[CommandDdd]
public sealed record AddSolutionElementCommand(SolutionToProblemId SolutionToProblemId, SolutionElement SolutionElement) 
    : IRequest<SolutionToProblemId>;