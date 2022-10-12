using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Solution.App.SolutionToProblem.Commands.ProblemCommands;

namespace the80by20.Solution.App.SolutionToProblem.CommandsHandlers.ProblemHandlers;

[CommandDdd]
public class RejectProblemCommandHandler : IRequestHandler<RejectProblemCommand, ProblemId>
{
    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly ProblemRejectionDomainService _problemRejectionDomainService;
    private readonly IMediator _mediator;

    public RejectProblemCommandHandler(
        IProblemAggregateRepository problemAggregateRepository,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        ProblemRejectionDomainService problemRejectionDomainService,
        IMediator mediator)
    {
        _problemAggregateRepository = problemAggregateRepository;
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _problemRejectionDomainService = problemRejectionDomainService;
        _mediator = mediator;
    }
    public async Task<ProblemId> Handle(RejectProblemCommand command, CancellationToken cancellationToken)
    {
        var problem = await _problemAggregateRepository.Get(command.ProblemId);
        problem = await _problemRejectionDomainService.RejectProblem(problem, _solutionToProblemAggregateRepository);
        await _problemAggregateRepository.SaveAggragate(problem);

        await UpdateReadModel(command.ProblemId, cancellationToken);

        return problem.Id;
    }

    private async Task UpdateReadModel(ProblemId problemId, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new ProblemUpdated(problemId), cancellationToken);
    }
}