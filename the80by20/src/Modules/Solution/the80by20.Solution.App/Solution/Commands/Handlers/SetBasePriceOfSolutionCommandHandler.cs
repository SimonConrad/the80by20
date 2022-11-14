
using the80by20.Modules.Solution.App.Solution.Commands;
using the80by20.Modules.Solution.App.Solution.Events;
using the80by20.Modules.Solution.Domain.Solution.DomainServicesPolicies;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Solution.App.Commands.Solution.Handlers;

public class SetBasePriceOfSolutionCommandHandler
    : ICommandHandler<SetBasePriceOfSolutionCommand>
{
    private readonly SetBasePriceForSolutionToProblemDomainService _domainService;
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IEventDispatcher _eventDispatcher;

    public SetBasePriceOfSolutionCommandHandler(SetBasePriceForSolutionToProblemDomainService domainService,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IEventDispatcher eventDispatcher)    
    {
        _domainService = domainService;
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task HandleAsync(SetBasePriceOfSolutionCommand command)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        _domainService.SetBasePrice(solution);
        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        await _eventDispatcher.PublishAsync(new UpdatedSolution(command.SolutionToProblemId));
    }

}