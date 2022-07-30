using MediatR;
using Microsoft.Extensions.DependencyInjection;
using the80by20.App.Core.SolutionToProblem.Events;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;

namespace the80by20.App.Core.SolutionToProblem.Commands.Handlers;

[CommandDdd]
public class UpdateProblemCommandHandler : IRequestHandler<UpdatProblemCommand, ProblemId>
{
    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly IServiceScopeFactory _servicesScopeFactory;

    public UpdateProblemCommandHandler(
        IProblemAggregateRepository problemAggregateRepository,
        IServiceScopeFactory servicesScopeFactory)
    {
        _problemAggregateRepository = problemAggregateRepository;
        _servicesScopeFactory = servicesScopeFactory;
    }
    public async Task<ProblemId> Handle(UpdatProblemCommand command, CancellationToken cancellationToken)
    {
        if (command.UpdateScope == UpdateDataScope.OnlyData)
        {
            await UpdateData(command);
            return command.ProblemId;
        }

        if (command.UpdateScope == UpdateDataScope.All)
        {
            await UpdateData(command);
        }

        var problem = await _problemAggregateRepository.Get(command.ProblemId);
        var requiredSolutionTypes = RequiredSolutionTypes.From(command.SolutionTypes);
        problem.Update(requiredSolutionTypes);
        await _problemAggregateRepository.SaveAggragate(problem);

        UpdateReadModel(_servicesScopeFactory, problem.Id);

        return problem.Id;
    }

    private async Task UpdateData(UpdatProblemCommand command)
    {
        var data = await _problemAggregateRepository.GetCrudData(command.ProblemId);
        data.Update(command.Description, command.Category);
        await _problemAggregateRepository.SaveData(data);
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