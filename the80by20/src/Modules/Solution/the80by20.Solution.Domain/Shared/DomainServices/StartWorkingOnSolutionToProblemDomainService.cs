using the80by20.Modules.Solution.Domain.Problem.Entities;
using the80by20.Modules.Solution.Domain.Solution.Entities;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Exceptions;

namespace the80by20.Modules.Solution.Domain.Shared.DomainServices;

[DomainServiceDdd]
public class StartWorkingOnSolutionToProblemDomainService
{
    [DomainServiceDdd]
    public SolutionToProblemAggregate StartWorkingOnSolutionToProblem(ProblemAggregate problemAggregate)
    {
        if (!problemAggregate.Confirmed)
            throw new DomainException("Cannot start working on not confirmed problem");

        if (problemAggregate.Rejected)
            throw new DomainException("Cannot start working on not rejected problem");

        if (!problemAggregate.RequiredSolutionTypes.Elements.Any())
            throw new DomainException("Cannot start working on solution, " +
                                      "when problem have no defined requirmed solution types");


        var solutionToProblemAggregate = SolutionToProblemAggregate.New(problemAggregate.Id.Value,
            problemAggregate.RequiredSolutionTypes.Copy());


        return solutionToProblemAggregate;
    }
}