using MediatR;
using the80by20.Modules.Solution.Domain.Operations.Problem;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Solution.App.Commands.Problem;

[CommandDdd]
public sealed record RejectProblemCommand(ProblemId ProblemId) : IRequest<ProblemId>;