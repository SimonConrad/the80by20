using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel.Capabilities;
using the80by20.Solution.Domain.Operations.Problem;

namespace the80by20.Solution.App.Commands.Problem;

[CommandDdd]
public sealed record CreateProblemCommand(string Description, Guid Category, Guid UserId, SolutionType[] SolutionElementTypes) : IRequest<ProblemId>;