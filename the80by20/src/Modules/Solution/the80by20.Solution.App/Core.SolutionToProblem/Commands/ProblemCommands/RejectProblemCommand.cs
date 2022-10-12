using MediatR;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.App.Core.SolutionToProblem.Commands.ProblemCommands;

[CommandDdd]
public sealed record RejectProblemCommand(ProblemId ProblemId) : IRequest<ProblemId>;