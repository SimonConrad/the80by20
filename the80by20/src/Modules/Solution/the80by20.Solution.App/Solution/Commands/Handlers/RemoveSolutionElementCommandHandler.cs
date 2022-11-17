﻿using the80by20.Modules.Solution.App.Solution.Events;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Solution.App.Solution.Commands.Handlers;

public class RemoveSolutionElementCommandHandler
    : ICommandHandler<RemoveSolutionElementCommand>
{
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IEventDispatcher _eventDispatcher;

    public RemoveSolutionElementCommandHandler(
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IEventDispatcher eventDispatcher)
    {
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task HandleAsync(RemoveSolutionElementCommand command)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        solution.RemoveSolutionElement(command.SolutionElement);

        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        await _eventDispatcher.PublishAsync(new UpdatedSolution(solution));
    }
}