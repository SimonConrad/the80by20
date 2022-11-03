using Shouldly;
using the80by20.Modules.Solution.Domain.Operations;
using the80by20.Modules.Solution.Domain.Operations.Problem;
using the80by20.Shared.Abstractions.Exceptions;

// info look at test from 3 perpsectives
// 1st testing like pure function give input and verify output
// 2nd checking state chnage setup aggregate in proper state (via getter or return of events), do command on it and veryfi i state was changed or invariants blocked (domain exception)
// 3rd verify interaction logic how flow goes - especially good for application logic setup mock s to behave properly and veryfy result, calls on mocks
// more info in the80by20 gdocs documments
// always try to veryfi stable api
namespace the80by20.Tests.Unit
{
    public class ProblemAggregateTests
    {
        [Fact]
        public void GIVEN_problem_without_required_solution_types_WHEN_calling_command_confirm_problem_THEN_should_command_fail()
        {
            // arrange
            var problem =
                ProblemAggregate.New(RequiredSolutionTypes.Empty());

            // act
            var exception = Record.Exception(() => problem.Confirm());

            // assert
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<DomainException>(); // todo custom exception
        }
    }
}