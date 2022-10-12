using MediatR;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Solution.App.Commands.SolutionCommands;
using the80by20.Solution.Domain.Operations.DomainServices;
using the80by20.Solution.Domain.Operations.Problem;
using the80by20.Solution.Domain.Operations.Solution;

namespace the80by20.Solution.App.CommandsHandlers.SolutionHandlers;

[CommandDdd]
public class StartWorkingOnSolutionCommandHandler
    : IRequestHandler<StartWorkingOnSolutionCommand, SolutionToProblemId>
{
    private readonly StartWorkingOnSolutionToProblemDomainService _domainService;
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly IMediator _mediator;


    public StartWorkingOnSolutionCommandHandler(StartWorkingOnSolutionToProblemDomainService domainService,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IProblemAggregateRepository problemAggregateRepository,
        IMediator mediator)
    {
        _domainService = domainService;
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _problemAggregateRepository = problemAggregateRepository;
        _mediator = mediator;
    }

    public async Task<SolutionToProblemId> Handle(StartWorkingOnSolutionCommand command,
        CancellationToken cancellationToken)
    {
        var problem = await _problemAggregateRepository.Get(command.ProblemId);
        var solution = _domainService.StartWorkingOnSolutionToProblem(problem);

        await _solutionToProblemAggregateRepository.Create(solution);

        await UpdateReadModel(solution.Id, cancellationToken);

        return solution.Id;
    }

    public async Task UpdateReadModel(SolutionToProblemId id, CancellationToken ct)
    {
        await _mediator.Publish(new StartedWorkingOnSolution(id), ct);
    }
}