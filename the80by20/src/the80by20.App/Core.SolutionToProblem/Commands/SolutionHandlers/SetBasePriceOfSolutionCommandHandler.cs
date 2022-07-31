using MediatR;
using Microsoft.Extensions.DependencyInjection;
using the80by20.App.Core.SolutionToProblem.Events;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Core.SolutionToProblem.Operations.DomainServices;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.App.Core.SolutionToProblem.Commands.SolutionHandlers;

[CommandDdd]
public class SetBasePriceOfSolutionCommandHandler 
    : IRequestHandler<SetBasePriceOfSolutionCommand, SolutionToProblemId>
{
    private readonly SetBasePriceForSolutionToProblemDomainService _domainService;
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IServiceScopeFactory _serviceScopeFactory;


    public SetBasePriceOfSolutionCommandHandler(SetBasePriceForSolutionToProblemDomainService domainService,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IServiceScopeFactory serviceScopeFactory)
    {
        _domainService = domainService;
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<SolutionToProblemId> Handle(SetBasePriceOfSolutionCommand command, 
        CancellationToken cancellationToken)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        _domainService.SetBasePrice(solution);

        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        UpdateReadModel(_serviceScopeFactory, solution.Id);

        return solution.Id;
    }

    public void UpdateReadModel(IServiceScopeFactory servicesScopeFactory, SolutionToProblemId id)
    {
        _ = Task.Run(async () =>
        {
            using var scope = servicesScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Publish(new UpdatedSolution(id));
        });
    }
}