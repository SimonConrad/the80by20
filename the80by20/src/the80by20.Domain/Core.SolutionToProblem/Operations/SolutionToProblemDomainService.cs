using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.Domain.Core.SolutionToProblem.Operations;

[DomainServiceDdd]
public class SolutionToProblemDomainService
{
    public SolutionToProblemAggregate StartWorkingOnSolutionToProblem(ProblemAggregate problemAggregate)
    {
        if (!problemAggregate.Confirmed)
            throw new DomainException("Cannot start working on not confirmed problem");

        if(problemAggregate.Rejected)
            throw new DomainException("Cannot start working on not rejected problem");

        if (!problemAggregate.RequiredSolutionElementTypes.Elements.Any())
            throw new DomainException("Cannot start working on solution, when problem have none defined req solution element types");


        var solutionToProblemAggregate =  SolutionToProblemAggregate.New(problemAggregate.Id,
            problemAggregate.RequiredSolutionElementTypes.Copy());

        solutionToProblemAggregate.StartWorkingOnProblemSolution();

        return solutionToProblemAggregate;
    }

    public async Task<ProblemAggregate> RejectProblem(ProblemAggregate problemAggregate, ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository)
    {
        if (await solutionToProblemAggregateRepository.IsTheSolutionAssignedToProblem(problemAggregate.Id))
            throw new DomainException("Cannot reject problem to which solution is assigned");

        problemAggregate.RejectProblem();

        return problemAggregate;

    }
}