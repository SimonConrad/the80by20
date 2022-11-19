using the80by20.Modules.Solution.App.Problem.Events;
using the80by20.Modules.Solution.Domain.Problem.Repositories;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Events;
using the80by20.Shared.Abstractions.Kernel;

namespace the80by20.Modules.Solution.App.Problem.Commands.Handlers;

public class ConfirmProblemCommandHandler : ICommandHandler<ConfirmProblemCommand>
{
    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly IEventDispatcher _eventDispatcher;

    public ConfirmProblemCommandHandler(
        IProblemAggregateRepository problemAggregateRepository,
        IDomainEventDispatcher domainEventDispatcher,
        IEventDispatcher eventDispatcher)
    {
        _problemAggregateRepository = problemAggregateRepository;
        _domainEventDispatcher = domainEventDispatcher;
        _eventDispatcher = eventDispatcher;
    }

    public async Task HandleAsync(ConfirmProblemCommand command)
    {
        var problem = await _problemAggregateRepository.Get(command.ProblemId);
        problem.Confirm();
        await _problemAggregateRepository.SaveAggragate(problem);

        await _eventDispatcher.PublishAsync(new ProblemUpdated(command.ProblemId, problem, null));
    }

}