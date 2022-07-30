using MediatR;
using Microsoft.Extensions.DependencyInjection;
using the80by20.App.Core.SolutionToProblem.Events;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.App.Core.SolutionToProblem.Commands.Handlers;

[CommandDdd]
public class StartWorkingOnSolutionToProblemCommandHandler 
    : IRequestHandler<StartWorkingOnSolutionToProblemCommand, SolutionToProblemId>
{
    private readonly StartWorkingOnSolutionToProblemDomainService _domainService;
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly IServiceScopeFactory _serviceScopeFactory;


    public StartWorkingOnSolutionToProblemCommandHandler(StartWorkingOnSolutionToProblemDomainService domainService,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IProblemAggregateRepository problemAggregateRepository,
        IServiceScopeFactory serviceScopeFactory)
    {
        _domainService = domainService;
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _problemAggregateRepository = problemAggregateRepository;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<SolutionToProblemId> Handle(StartWorkingOnSolutionToProblemCommand command, 
        CancellationToken cancellationToken)
    {
        var problem = await _problemAggregateRepository.Get(command.ProblemId);
        var solution = _domainService.StartWorkingOnSolutionToProblem(problem);

        await _solutionToProblemAggregateRepository.Create(solution);

        UpdateReadModel(_serviceScopeFactory, solution.Id);

        return solution.Id;
    }

    public void UpdateReadModel(IServiceScopeFactory servicesScopeFactory, SolutionToProblemId id)
    {
        _ = Task.Run(async () =>
        {
            using var scope = servicesScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Publish(new StartedWorkingOnSolutionToProblem(id));
        });
    }
}