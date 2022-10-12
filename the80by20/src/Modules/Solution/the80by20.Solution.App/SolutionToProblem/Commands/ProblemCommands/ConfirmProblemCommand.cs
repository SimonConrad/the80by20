using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Solution.Domain.SolutionToProblem.Operations.Problem;

namespace the80by20.Solution.App.SolutionToProblem.Commands.ProblemCommands;

[CommandDdd]
public sealed record ConfirmProblemCommand(ProblemId ProblemId) : IRequest<ProblemId>;