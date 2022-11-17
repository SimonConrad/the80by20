using Shouldly;
using the80by20.Modules.Solution.Domain.Problem.Entities;
using the80by20.Modules.Solution.Domain.Problem.Exceptions;
using the80by20.Modules.Solution.Domain.Shared;

// INFO
// look at test from 3 perspectives
//
// 1.like testing pure function - give input and verify output
//
// 2.check if state changed correctly
//   call a command (public api of teste object) and verify if state was changed properly or invariants blocked state change (by checking if domain exception thrown)
//   verify state chnage via getter or collection of events in aggregate
//
// 3.verify if interaction logic flow goes correctly (application logic tests)
// setup mock so it behaves properly and verify result  (like if proper number of calls on mocks were done)

// always try to tests stable api - so that test is not fragile to refactor, stable api == buisness requierments

namespace the80by20.Tests.Unit
{
    public class ProblemAggregateTests
    {
        [Fact]
        public void GIVEN_problem_without_required_solution_types_WHEN_calling_command_confirm_problem_THEN_should_command_fail()
        {
            // arrange
            var problem =
                ProblemAggregate.New(Guid.NewGuid(), RequiredSolutionTypes.Empty());

            // act
            var exception = Record.Exception(() => problem.Confirm());

            // assert
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ProblemException>(); // todo custom exception
        }
    }
}