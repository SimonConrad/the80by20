using the80by20.Common.ArchitectureBuildingBlocks.Exceptions;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.Domain.Core.SolutionToProblem.Operations.DomainServices;

[DomainServiceDdd]
public class ProblemRejectionDomainService
{
    public async Task<ProblemAggregate> RejectProblem(ProblemAggregate problemAggregate, 
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository)
    {
        if (await solutionToProblemAggregateRepository.IsTheSolutionAssignedToProblem(problemAggregate.Id))
            throw new DomainException("Cannot reject problem to which solution is assigned");

        problemAggregate.Reject();

        return problemAggregate;
    }
}