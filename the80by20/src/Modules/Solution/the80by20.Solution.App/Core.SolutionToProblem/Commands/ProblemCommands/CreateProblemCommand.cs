using MediatR;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel.Capabilities;

namespace the80by20.App.Core.SolutionToProblem.Commands.ProblemCommands;

[CommandDdd]
public sealed record CreateProblemCommand(string Description, Guid Category, Guid UserId, SolutionType[] SolutionElementTypes) : IRequest<ProblemId>;