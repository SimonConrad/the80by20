using the80by20.Modules.Solution.Domain.Problem.Entities;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Solution.Domain.Shared.DomainServices;

[DomainServiceDdd]
public class ProblemRejectionDomainService
{
    public async Task<ProblemAggregate> RejectProblem(ProblemAggregate problemAggregate,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository)
    {

        // TODO pass aggragate SolutionToProblemAggregate to this domain-service method, no its irepository and operate on aggragate
        // chnage below to if(solutionToProblemAggregate.sTheSolutionAssignedToProblem())
        // remove method IsTheSolutionAssignedToProblem from repo

        if (await solutionToProblemAggregateRepository.IsTheSolutionAssignedToProblem(problemAggregate.Id.Value))
            throw new DomainException("Cannot reject problem to which solution is assigned");

        problemAggregate.Reject();

        return problemAggregate;
    }
}