using NSubstitute;
using the80by20.Modules.Solution.App.Solution.Commands;
using the80by20.Modules.Solution.App.Solution.Commands.Handlers;
using the80by20.Modules.Solution.App.Solution.Events;
using the80by20.Modules.Solution.App.Solution.Services;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Modules.Solution.Domain.Solution.Entities;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Modules.Solution.Domain.Solution.ValueObjects;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Events;
using the80by20.Shared.Abstractions.Kernel;
using the80by20.Shared.Abstractions.Messaging;

namespace the80by20.Tests.Unit.Solution.Commands.Handlers
{
    public class FinishSolutionCommandHandlerTests
    {
        private Task Act(FinishSolutionCommand command) => _handler.HandleAsync(command);

        [Fact]
        public async Task Test_FinishSolutionCommand_HappyPath()
        {
            // arrange
            var command = new FinishSolutionCommand(Guid.NewGuid());
            var solution = GetReadyToBeFinishedSolution();
            _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId).Returns(solution);

            // act
            await Act(command);


            // asserts
            // INFO
            // Finish solution aggregate and persists it
            await _solutionToProblemAggregateRepository.Received(1)
                .SaveAggragate(Arg.Is<SolutionToProblemAggregate>(x => x.WorkingOnSolutionEnded));

            // INFO
            // dispatch domain-event to archive problem (by soft-delete, end-of-life of the problem-aggregate object happens)
            // dispatch SolutionFinished event which is handled by SolutionFinishedHandler in module problem, in its' domain layer
            await _domainEventDispatcher.Received(1).DispatchAsync(Arg.Is<IDomainEvent[]>(x => 
                x.Count() == 1
                && x[0].GetType() == typeof(the80by20.Modules.Solution.Domain.Solution.Events.SolutionFinished)
            ));

            // INFO
            // publish an integration-event SolutionFinished to module sale
            // handling logic: aggregate solution becomes aggregate product in sale module
            await _messageBroker.Received(1).PublishAsync(Arg.Is<IMessage[]>(x => 
                x.Count() == 1
                && x[0].GetType() == typeof(the80by20.Modules.Solution.App.Solution.Events.SolutionFinished)));

            // INFO
            // publish and application-event to update solution-to-problem readmodel
            await _eventDispatcher.Received(1).PublishAsync(Arg.Any<UpdatedSolution>());

        }

        private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;
        private ICommandHandler<FinishSolutionCommand> _handler;

        public FinishSolutionCommandHandlerTests()
        {
            _solutionToProblemAggregateRepository = Substitute.For<ISolutionToProblemAggregateRepository>();
            _domainEventDispatcher = Substitute.For<IDomainEventDispatcher>();
            _eventDispatcher = Substitute.For<IEventDispatcher>();
            _eventMapper = new EventMapper();
            _messageBroker = Substitute.For<IMessageBroker>();

            _handler = new FinishSolutionCommandHandler(_solutionToProblemAggregateRepository,
                _messageBroker,
                _domainEventDispatcher,
                _eventDispatcher,
                _eventMapper);
        }

        private static SolutionToProblemAggregate GetReadyToBeFinishedSolution()
        {
            var solution = SolutionToProblemAggregate.New(Guid.NewGuid(), RequiredSolutionTypes.From(new SolutionType[] { SolutionType.TheoryOfConceptWithExample }));
            solution.SetBasePrice(100.00m);
            solution.SetSummary(SolutionSummary.FromContent("bla bla bla"));
            solution.AddSolutionElement(SolutionElement.From(SolutionType.TheoryOfConceptWithExample, "www.drive.com/solution001"));

            return solution;
        }
    }
}
