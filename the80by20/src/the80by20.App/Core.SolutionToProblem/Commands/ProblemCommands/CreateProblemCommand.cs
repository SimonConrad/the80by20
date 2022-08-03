using MediatR;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.SharedKernel.Capabilities;

namespace the80by20.App.Core.SolutionToProblem.Commands.ProblemCommands;

[CommandDdd]
public sealed record CreateProblemCommand(string Description, Guid Category, Guid UserId, SolutionType[] SolutionElementTypes) : IRequest<ProblemId>;