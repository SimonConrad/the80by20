using Shouldly;
using the80by20.Modules.Solution.Domain.Problem.Entities;
using the80by20.Modules.Solution.Domain.Problem.Exceptions;
using the80by20.Modules.Solution.Domain.Shared;

// INFO
// look at test from 3 perspectives
//
// (1)  like testing pure function - give input and verify output
//
// (2)  check if state changed correctly
//      call a command (public api of teste object) and verify if state was changed properly or invariants blocked state change (by checking if domain exception thrown)
//      verify state chnage via getter or collection of events in aggregate
//
// (3)  verify if interaction logic flow goes correctly (application logic tests)
//      setup mock so it behaves properly and verify result  (like if proper number of calls on mocks were done)

// always try to tests stable api - so that test are not fragile to refactor, stable api == buisness requierments

// kind of tests:
// (1)  unit tests - testing single componentes (usually classes) in isolated environment (isolation is achieved often by using mocks),
//      ex testing aggregates, valueobject, domain-service or application handler
//      - mocking / london school - we provide mocks to our unit of tests
//      - classic school - we provide actual implemntations of dependencies of our unit of tests
//
//
// (2)  integration tests - testing points of connection (communication) between two or more modules
//
// (3)  end to end tests - testing whole user use cases in the context of integration chosen group of components (ex. whole http request in api)

namespace the80by20.Tests.Unit.Solution.Entities
{
    public class ProblemAggregateTests : IDisposable
    {
        public ProblemAggregateTests()
        {
            // INFO
            // code (in construtor) run before each test, in nunit setup attribute
            // place for initialization for each tests
        }

        public void Dispose()
        {
            // INFO
            // run after each test
        }

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
            exception.ShouldBeOfType<NoRequiredSolutionTypesException>();
        }

        // INFO
        //[Theory]
        //[MemberData(nameof(GetCollidingDates))]
        //public void Fake(DateTime from, DateTime to)
        //{

        //}

        //public static IEnumerable<object[]> GetCollidingDates()
        //{
        //    yield return new object[] { DateTime.Now, DateTime.Now };
        //    yield return new object[] { DateTime.Now, DateTime.Now };
        //    yield return new object[] { DateTime.Now, DateTime.Now };
        //    yield return new object[] { DateTime.Now, DateTime.Now };
        //}
    }
}