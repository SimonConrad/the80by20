using MediatR;
using the80by20.Modules.Solution.App.Events.External;
using the80by20.Modules.Solution.App.Events.Solution;
using the80by20.Modules.Solution.Domain.Operations.Solution;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Events;
using the80by20.Shared.Abstractions.Modules;

namespace the80by20.Modules.Solution.App.Commands.Solution.Handlers;

[CommandDdd]
public class FinishSolutionCommandHandler
    : IRequestHandler<FinishSolutionCommand, SolutionToProblemId>
{
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IMediator _mediator;
    private readonly IModuleClient _moduleClient;

    public FinishSolutionCommandHandler(ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IMediator mediator,
        IModuleClient moduleClient)
    {
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _mediator = mediator;
        _moduleClient = moduleClient;
    }

    public async Task<SolutionToProblemId> Handle(FinishSolutionCommand command,
        CancellationToken cancellationToken)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        solution.FinishWorkOnSolutionToProblem();
        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        await UpdateReadModel(solution.Id, cancellationToken);

        // todo: await _moduleClient.PublishAsync(new SolutionToProblemFinished(Guid.NewGuid(), Guid.NewGuid(), "", "", 0));

        return solution.Id;
    }

    public async Task UpdateReadModel(SolutionToProblemId id, CancellationToken ct)
    {
        await _mediator.Publish(new UpdatedSolution(id), ct);
    }
}