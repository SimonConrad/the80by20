using MediatR;
using the80by20.App.Core.SolutionToProblem.Commands.SolutionCommands;
using the80by20.App.Core.SolutionToProblem.Events.SolutionEvents;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.App.Core.SolutionToProblem.CommandsHandlers.SolutionHandlers;

[CommandDdd]
public class AddSolutionElementCommandHandler 
    : IRequestHandler<AddSolutionElementCommand, SolutionToProblemId>
{
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IMediator _mediator;

    public AddSolutionElementCommandHandler(
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IMediator mediator)
    {
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _mediator = mediator;
    }

    public async Task<SolutionToProblemId> Handle(AddSolutionElementCommand command, 
        CancellationToken cancellationToken)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        solution.AddSolutionElement(command.SolutionElement);
        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        await UpdateReadModel(solution.Id, cancellationToken);

        return solution.Id;
    }

    public async Task UpdateReadModel(SolutionToProblemId id, CancellationToken ct)
    {
        await _mediator.Publish(new UpdatedSolution(id), ct);
    }
}