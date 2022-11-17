using the80by20.Modules.Solution.App.Solution.Events;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Solution.App.Solution.Commands.Handlers;

public class SetSolutionSummaryCommandHandler
    : ICommandHandler<SetSolutionSummaryCommand>
{
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IEventDispatcher _eventDispatcher;

    public SetSolutionSummaryCommandHandler(ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository
, IEventDispatcher eventDispatcher)
    {
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task HandleAsync(SetSolutionSummaryCommand command)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        solution.SetSummary(command.SolutionSummary);
        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        await _eventDispatcher.PublishAsync(new UpdatedSolution(solution));

    }
}