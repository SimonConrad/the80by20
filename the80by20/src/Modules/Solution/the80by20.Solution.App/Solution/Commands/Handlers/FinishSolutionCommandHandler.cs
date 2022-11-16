using the80by20.Modules.Solution.App.Solution.Events;
using the80by20.Modules.Solution.App.Solution.Services;
using the80by20.Modules.Solution.Domain.Solution.Entities;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Events;
using the80by20.Shared.Abstractions.Kernel;
using the80by20.Shared.Abstractions.Messaging;

namespace the80by20.Modules.Solution.App.Solution.Commands.Handlers;


public class FinishSolutionCommandHandler
    : ICommandHandler<FinishSolutionCommand>
{
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;

    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IEventMapper _eventMapper;
    private readonly IMessageBroker _messageBroker;

    public FinishSolutionCommandHandler(ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IMessageBroker messageBroker,
        IDomainEventDispatcher domainEventDispatcher,
        IEventDispatcher eventDispatcher,
        IEventMapper eventMapper)
    {
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository; ;
        _messageBroker = messageBroker;
        _domainEventDispatcher = domainEventDispatcher;
        _eventDispatcher = eventDispatcher;
        _eventMapper = eventMapper;
    }

    public async Task HandleAsync(FinishSolutionCommand command)
    {
        SolutionToProblemAggregate solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        solution.FinishWorkOnSolutionToProblem();
        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        // INFO
        // approach with shared contracts:
        // add project the80by20.Modules.Solution.Messages and there in catalog Events add SolutionToProblemFinished
        // add above project as dependency in Sale.App
        // in Solution module send event via
        // await IEventDispatcher.PublishAsync(new SolutionToProblemFinished(Guid.NewGuid(), Guid.NewGuid(), "", "", 0));
        // pros: quite easy, cons: coupling: sale module hase dependecy on solution module (project reference)

        // TODO: do following 

        // INFO
        // published to archive problem (soft-delete) - end-of-life of the problem-aggregate object 
        // event: SolutionFinished
        await _domainEventDispatcher.DispatchAsync(solution.Events.ToArray());

        // INFO
        // published to create product entity - handled in sale module // event: SolutionFinished
        // perspective *becomes* aggregate solution becomes aggregate product
        var integrationEvents = _eventMapper.MapAll(solution.Events);
        await _messageBroker.PublishAsync(integrationEvents.ToArray());

        // INFO
        // published to update read-model - optimized for reads, denormalized, eventuall consistnet (not immediate) on purpose
        await _eventDispatcher.PublishAsync(new UpdatedSolution(command.SolutionToProblemId));
    }
}