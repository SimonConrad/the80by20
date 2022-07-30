using MediatR;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.SharedKernel.Capabilities;

namespace the80by20.App.Core.SolutionToProblem.Commands
{
    [CommandDdd]
    public sealed record RejectProblemCommand(ProblemId ProblemId) : IRequest<ProblemId>;
}
