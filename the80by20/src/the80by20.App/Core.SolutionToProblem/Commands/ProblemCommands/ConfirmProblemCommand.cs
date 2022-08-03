using MediatR;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;

namespace the80by20.App.Core.SolutionToProblem.Commands.ProblemCommands;

[CommandDdd]
public sealed record ConfirmProblemCommand(ProblemId ProblemId) : IRequest<ProblemId>;