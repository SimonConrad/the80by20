using MediatR;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.SharedKernel.Capabilities;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.App.Core.SolutionToProblem.Commands.ProblemCommands;

[CommandDdd]
public sealed record UpdateProblemCommand(Guid ProblemId, string Description, Guid Category, SolutionType[] SolutionTypes, UpdateDataScope UpdateScope) : IRequest<ProblemId>;

public enum UpdateDataScope
{
    All,
    OnlyData,
    OnlySolutionTypes
}