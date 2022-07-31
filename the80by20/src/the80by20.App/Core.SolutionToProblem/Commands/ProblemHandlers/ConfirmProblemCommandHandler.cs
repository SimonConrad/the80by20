using MediatR;
using Microsoft.Extensions.DependencyInjection;
using the80by20.App.Core.SolutionToProblem.Events;
using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;

namespace the80by20.App.Core.SolutionToProblem.Commands.ProblemHandlers;

[CommandDdd]
public class ConfirmProblemCommandHandler : IRequestHandler<ConfirmProblemCommand, ProblemId>
{
    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly IServiceScopeFactory _servicesScopeFactory;

    public ConfirmProblemCommandHandler(
        IProblemAggregateRepository problemAggregateRepository,
        IServiceScopeFactory servicesScopeFactory)
    {
        _problemAggregateRepository = problemAggregateRepository;
        _servicesScopeFactory = servicesScopeFactory;
    }
    public async Task<ProblemId> Handle(ConfirmProblemCommand command, CancellationToken cancellationToken)
    {
        var problem = await _problemAggregateRepository.Get(command.ProblemId);
        problem.Confirm();
        await _problemAggregateRepository.SaveAggragate(problem);


        UpdateReadModel(_servicesScopeFactory, problem.Id);

        return problem.Id;
    }

    public void UpdateReadModel(IServiceScopeFactory servicesScopeFactory, ProblemId id)
    {
        _ = Task.Run(async () =>
        {
            using var scope = servicesScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Publish(new ProblemUpdated(id));
        });
    }
}