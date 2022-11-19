using the80by20.Modules.Solution.App.Problem.Events;
using the80by20.Modules.Solution.Domain.Problem.Entities;
using the80by20.Modules.Solution.Domain.Problem.Repositories;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Solution.App.Problem.Commands.Handlers;

public class UpdateProblemCommandHandler : ICommandHandler<UpdateProblemCommand>
{
    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly IEventDispatcher _eventDispatcher;

    public UpdateProblemCommandHandler(
        IProblemAggregateRepository problemAggregateRepository,
        IEventDispatcher eventDispatcher)
    {
        _problemAggregateRepository = problemAggregateRepository;
        _eventDispatcher = eventDispatcher;
    }

    // TODO refactor
    public async Task HandleAsync(UpdateProblemCommand command)
    {
        if (command.UpdateScope == UpdateDataScope.OnlyData)
        {
            ProblemCrudData data = await UpdateData(command);
            await _eventDispatcher.PublishAsync(new ProblemUpdated(command.ProblemId, null, data));
            return;
        }

        ProblemCrudData data1 = null;
        if (command.UpdateScope == UpdateDataScope.All)
        {
            await UpdateData(command);
        }

        var problem = await _problemAggregateRepository.Get(command.ProblemId);
        var requiredSolutionTypes = RequiredSolutionTypes.From(command.SolutionTypes);
        problem.Update(requiredSolutionTypes);
        await _problemAggregateRepository.SaveAggragate(problem);

        await _eventDispatcher.PublishAsync(new ProblemUpdated(command.ProblemId, problem, data1));
    }

    private async Task<ProblemCrudData> UpdateData(UpdateProblemCommand command)
    {
        var data = await _problemAggregateRepository.GetCrudData(command.ProblemId);
        data.Update(command.Description, command.Category);
        await _problemAggregateRepository.SaveData(data);

        return data;
    }
}