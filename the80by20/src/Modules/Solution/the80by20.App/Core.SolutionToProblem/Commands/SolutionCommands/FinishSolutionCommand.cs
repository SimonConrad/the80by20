using MediatR;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.App.Core.SolutionToProblem.Commands.SolutionCommands;

[CommandDdd]
public sealed record FinishSolutionCommand(SolutionToProblemId SolutionToProblemId) 
    : IRequest<SolutionToProblemId>;

