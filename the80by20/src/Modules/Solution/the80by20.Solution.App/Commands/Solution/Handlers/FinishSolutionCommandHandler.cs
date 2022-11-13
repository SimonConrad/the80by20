using MediatR;
using the80by20.Modules.Solution.App.Events.External;
using the80by20.Modules.Solution.App.Events.Solution;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Events;
using the80by20.Shared.Abstractions.Kernel;
using the80by20.Shared.Abstractions.Kernel.Types;
using the80by20.Shared.Abstractions.Messaging;
using the80by20.Shared.Abstractions.Modules;

namespace the80by20.Modules.Solution.App.Commands.Solution.Handlers;

[CommandDdd]
public class FinishSolutionCommandHandler
    : IRequestHandler<FinishSolutionCommand, SolutionToProblemId>
{
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IMediator _mediator;

    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly IMessageBroker _messageBroker;

    public FinishSolutionCommandHandler(ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IMediator mediator,
        IMessageBroker messageBroker,
        IDomainEventDispatcher domainEventDispatcher)
    {
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _mediator = mediator;
        _messageBroker = messageBroker;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task<SolutionToProblemId> Handle(FinishSolutionCommand command,
        CancellationToken cancellationToken)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        solution.FinishWorkOnSolutionToProblem();
        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        await UpdateReadModel(solution.Id.Value, cancellationToken);

        // INFO
        // approach with shared contracts:
        // add project the80by20.Modules.Solution.Messages and there in catalog Events add SolutionToProblemFinished
        // add above project as dependency in Sale.App
        // in Solution module send event via
        // await IEventDispatcher.PublishAsync(new SolutionToProblemFinished(Guid.NewGuid(), Guid.NewGuid(), "", "", 0));
        // pros: quite easy, cons: coupling: sale module hase dependecy on solution module (project reference)

        //todo: 
        await _messageBroker.PublishAsync(new SolutionToProblemFinished(Guid.NewGuid(), Guid.NewGuid(), "", "", 0));

        return solution.Id.Value;
    }

    public async Task UpdateReadModel(SolutionToProblemId id, CancellationToken ct)
    {
        await _mediator.Publish(new UpdatedSolution(id), ct);
    }
}