using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Solution.App.SolutionToProblem.Commands.SolutionCommands;
using the80by20.Solution.App.SolutionToProblem.Events.SolutionEvents;
using the80by20.Solution.Domain.SolutionToProblem.Operations.Solution;

namespace the80by20.Solution.App.SolutionToProblem.CommandsHandlers.SolutionHandlers;

[CommandDdd]
public class SetAdditionalPriceCommandHandler
    : IRequestHandler<SetAdditionalPriceCommand, SolutionToProblemId>
{
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IMediator _mediator;

    public SetAdditionalPriceCommandHandler(ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IMediator mediator)
    {
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _mediator = mediator;
    }

    public async Task<SolutionToProblemId> Handle(SetAdditionalPriceCommand command,
        CancellationToken cancellationToken)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        solution.SetAdditionalPrice(command.AdditionalPrice);
        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        await UpdateReadModel(solution.Id, cancellationToken);

        return solution.Id;
    }

    public async Task UpdateReadModel(SolutionToProblemId id, CancellationToken ct)
    {
        await _mediator.Publish(new UpdatedSolution(id), ct);
    }
}