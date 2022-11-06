using MediatR;
using the80by20.Modules.Solution.App.Events.Solution;
using the80by20.Modules.Solution.Domain.Operations.Solution;
using the80by20.Modules.Solution.Messages.Events;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Solution.App.Commands.Solution.Handlers;

[CommandDdd]
public class FinishSolutionCommandHandler
    : IRequestHandler<FinishSolutionCommand, SolutionToProblemId>
{
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IMediator _mediator;
    private readonly IEventDispatcher _eventDispatcher;

    public FinishSolutionCommandHandler(ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IMediator mediator,
        IEventDispatcher eventDispatcher)
    {
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _mediator = mediator;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<SolutionToProblemId> Handle(FinishSolutionCommand command,
        CancellationToken cancellationToken)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        solution.FinishWorkOnSolutionToProblem();
        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        await UpdateReadModel(solution.Id, cancellationToken);

        // todo: await _eventDispatcher.PublishAsync(new SolutionToProblemFinished(Guid.NewGuid(), Guid.NewGuid(), "", "", 0));
        // above is happen synchronously if want asynchronous call without await ore use _eventDispatcher.Publish

        return solution.Id;
    }

    public async Task UpdateReadModel(SolutionToProblemId id, CancellationToken ct)
    {
        await _mediator.Publish(new UpdatedSolution(id), ct);
    }
}