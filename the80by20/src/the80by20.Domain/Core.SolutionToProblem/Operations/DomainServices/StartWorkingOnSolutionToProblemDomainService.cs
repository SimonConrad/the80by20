using the80by20.Common.ArchitectureBuildingBlocks;
using the80by20.Common.ArchitectureBuildingBlocks.Exceptions;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.Domain.Core.SolutionToProblem.Operations.DomainServices;

[DomainServiceDdd]
public class StartWorkingOnSolutionToProblemDomainService
{
    [DomainServiceDdd]
    public SolutionToProblemAggregate StartWorkingOnSolutionToProblem(ProblemAggregate problemAggregate)
    {
        if (!problemAggregate.Confirmed)
            throw new DomainException("Cannot start working on not confirmed problem");

        if(problemAggregate.Rejected)
            throw new DomainException("Cannot start working on not rejected problem");

        if (!problemAggregate.RequiredSolutionTypes.Elements.Any())
            throw new DomainException("Cannot start working on solution, " +
                                      "when problem have no defined requirmed solution types");


        var solutionToProblemAggregate =  SolutionToProblemAggregate.New(problemAggregate.Id,
            problemAggregate.RequiredSolutionTypes.Copy());


        return solutionToProblemAggregate;
    }
}