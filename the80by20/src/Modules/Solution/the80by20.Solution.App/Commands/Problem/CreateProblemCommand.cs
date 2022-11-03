using MediatR;
using the80by20.Modules.Solution.Domain.Operations.Problem;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.SharedKernel.Capabilities;

namespace the80by20.Modules.Solution.App.Commands.Problem;

[CommandDdd]
public sealed record CreateProblemCommand(string Description, Guid Category, Guid UserId, SolutionType[] SolutionElementTypes) : IRequest<ProblemId>;