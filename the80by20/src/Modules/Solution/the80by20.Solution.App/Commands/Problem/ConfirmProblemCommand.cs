using MediatR;
using the80by20.Modules.Solution.Domain.Problem;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.Commands.Problem;

[CommandDdd]
public sealed record ConfirmProblemCommand(ProblemId ProblemId) : IRequest<ProblemId>;