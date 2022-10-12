using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.Exceptions;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Solution.Domain.Operations.Problem;
using the80by20.Solution.Domain.Operations.Solution;

namespace the80by20.Solution.Domain.Operations.DomainServices;

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