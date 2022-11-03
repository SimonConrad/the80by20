using MediatR;
using the80by20.Modules.Solution.Domain.Operations.Problem;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.SharedKernel.Capabilities;

namespace the80by20.Modules.Solution.App.Commands.Problem;

[CommandDdd]
public sealed record UpdateProblemCommand(Guid ProblemId, string Description, Guid Category, SolutionType[] SolutionTypes, UpdateDataScope UpdateScope) : IRequest<ProblemId>;

public enum UpdateDataScope
{
    All,
    OnlyData,
    OnlySolutionTypes
}