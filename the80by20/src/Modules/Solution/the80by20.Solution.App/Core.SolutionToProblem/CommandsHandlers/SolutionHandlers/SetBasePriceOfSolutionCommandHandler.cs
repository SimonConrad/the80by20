using MediatR;
using the80by20.App.Core.SolutionToProblem.Commands.SolutionCommands;
using the80by20.App.Core.SolutionToProblem.Events.SolutionEvents;
using the80by20.Domain.Core.SolutionToProblem.Operations.DomainServices;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.App.Core.SolutionToProblem.CommandsHandlers.SolutionHandlers;

[CommandDdd]
public class SetBasePriceOfSolutionCommandHandler 
    : IRequestHandler<SetBasePriceOfSolutionCommand, SolutionToProblemId>
{
    private readonly SetBasePriceForSolutionToProblemDomainService _domainService;
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IMediator _mediator;

    public SetBasePriceOfSolutionCommandHandler(SetBasePriceForSolutionToProblemDomainService domainService,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IMediator mediator)
    {
        _domainService = domainService;
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _mediator = mediator;
    }

    public async Task<SolutionToProblemId> Handle(SetBasePriceOfSolutionCommand command, 
        CancellationToken cancellationToken)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        _domainService.SetBasePrice(solution);
        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        await UpdateReadModel(solution.Id, cancellationToken);

        return solution.Id;
    }

    public async Task UpdateReadModel(SolutionToProblemId id, CancellationToken ct)
    {
        await _mediator.Publish(new UpdatedSolution(id), ct);
    }
}