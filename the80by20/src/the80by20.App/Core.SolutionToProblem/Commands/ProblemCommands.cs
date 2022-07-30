using MediatR;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.SharedKernel.Capabilities;

namespace the80by20.App.Core.SolutionToProblem.Commands
{

    [CommandDdd]
    public sealed record CreateProblemCommand(string Description, 
        Guid Category, 
        SolutionType[] SolutionElementTypes) : IRequest<ProblemId>;

    [CommandDdd]
    public sealed record RejectProblemCommand(ProblemId ProblemId) : IRequest<ProblemId>;

    [CommandDdd]
    public sealed record ConfirmProblemCommand(ProblemId ProblemId) : IRequest<ProblemId>;

    [CommandDdd]
    public sealed record UpdatProblemCommand(Guid ProblemId,
        string Description,
        Guid Category,
        SolutionType[] SolutionTypes,
        UpdateDataScope UpdateScope
    ) : IRequest<ProblemId>;

    public enum UpdateDataScope
    {
        All,
        OnlyData,
        OnlySolutionTypes
    }
}
