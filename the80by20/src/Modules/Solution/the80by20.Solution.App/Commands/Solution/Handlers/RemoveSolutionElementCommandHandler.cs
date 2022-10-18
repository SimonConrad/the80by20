using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Solution.App.Commands.Solution;
using the80by20.Solution.App.Events.SolutionEvents;
using the80by20.Solution.Domain.Operations.Solution;

namespace the80by20.Solution.App.Commands.Solution.Handlers;

[CommandDdd]
public class RemoveSolutionElementCommandHandler
    : IRequestHandler<RemoveSolutionElementCommand, SolutionToProblemId>
{
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IMediator _mediator;

    public RemoveSolutionElementCommandHandler(ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IMediator mediator)
    {
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _mediator = mediator;
    }

    public async Task<SolutionToProblemId> Handle(RemoveSolutionElementCommand command,
        CancellationToken cancellationToken)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        solution.RemoveSolutionElement(command.SolutionElement);

        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        await UpdateReadModel(solution.Id, cancellationToken);

        return solution.Id;
    }

    public async Task UpdateReadModel(SolutionToProblemId id, CancellationToken ct)
    {
        await _mediator.Publish(new UpdatedSolution(id), ct);
    }
}