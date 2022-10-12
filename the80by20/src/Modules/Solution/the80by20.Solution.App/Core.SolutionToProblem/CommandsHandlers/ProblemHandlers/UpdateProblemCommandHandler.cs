using MediatR;
using the80by20.App.Core.SolutionToProblem.Commands.ProblemCommands;
using the80by20.App.Core.SolutionToProblem.Events.ProblemEvents;
using the80by20.Domain.Core.SolutionToProblem.Operations;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.App.Core.SolutionToProblem.CommandsHandlers.ProblemHandlers;

[CommandDdd]
public class UpdateProblemCommandHandler : IRequestHandler<UpdateProblemCommand, ProblemId>
{
    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly IMediator _mediator;

    public UpdateProblemCommandHandler(IProblemAggregateRepository problemAggregateRepository,
        IMediator mediator)
    {
        _problemAggregateRepository = problemAggregateRepository;
        _mediator = mediator;
    }

    public async Task<ProblemId> Handle(UpdateProblemCommand command, CancellationToken cancellationToken)
    {
        if (command.UpdateScope == UpdateDataScope.OnlyData)
        {
            await UpdateData(command);
            await UpdateReadModel(command.ProblemId, cancellationToken);
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

        await UpdateReadModel(problem.Id, cancellationToken);

        return problem.Id;
    }

    private async Task UpdateData(UpdateProblemCommand command)
    {
        var data = await _problemAggregateRepository.GetCrudData(command.ProblemId);
        data.Update(command.Description, command.Category);
        await _problemAggregateRepository.SaveData(data);
    }

    public async Task UpdateReadModel(ProblemId id, CancellationToken ct)
    {
        await _mediator.Publish(new ProblemUpdated(id), ct);
    }
}