using MediatR;
using the80by20.Modules.Solution.App.Problem.Commands;
using the80by20.Modules.Solution.App.Problem.Events;
using the80by20.Modules.Solution.Domain.Problem.Repositories;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.Commands.Problem.Handlers;

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

        await UpdateReadModel(problem.Id.Value, cancellationToken);

        return problem.Id.Value;
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