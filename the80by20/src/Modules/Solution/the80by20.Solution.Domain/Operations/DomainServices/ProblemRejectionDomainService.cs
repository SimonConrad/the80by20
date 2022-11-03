﻿using the80by20.Modules.Solution.Domain.Operations.Problem;
using the80by20.Modules.Solution.Domain.Operations.Solution;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Solution.Domain.Operations.DomainServices;

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