using the80by20.Modules.Solution.App.Solution.Events;
using the80by20.Modules.Solution.Domain.Solution.DomainServices;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Events;
using the80by20.Shared.Abstractions.Time;

namespace the80by20.Modules.Solution.App.Solution.Commands.Handlers;

public class SetBasePriceOfSolutionCommandHandler
    : ICommandHandler<SetBasePriceOfSolutionCommand>
{
    private readonly SetBasePriceForSolutionDomainService _domainService;
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IClock _clock;

    public SetBasePriceOfSolutionCommandHandler(SetBasePriceForSolutionDomainService domainService,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IEventDispatcher eventDispatcher,
        IClock clock)
    {
        _domainService = domainService;
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _eventDispatcher = eventDispatcher;
        _clock = clock;
    }

    public async Task HandleAsync(SetBasePriceOfSolutionCommand command)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        _domainService.SetBasePrice(solution, _clock);
        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        await _eventDispatcher.PublishAsync(new UpdatedSolution(solution));
    }

}