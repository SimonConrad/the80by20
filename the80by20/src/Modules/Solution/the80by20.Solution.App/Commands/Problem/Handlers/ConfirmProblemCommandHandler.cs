﻿using MediatR;
using the80by20.Modules.Solution.App.Events.Problem;
using the80by20.Modules.Solution.Domain.Operations.Problem;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Solution.App.Commands.Problem.Handlers;

[CommandDdd]
public class ConfirmProblemCommandHandler : IRequestHandler<ConfirmProblemCommand, ProblemId>
{
    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly IMediator _mediator;

    public ConfirmProblemCommandHandler(
        IProblemAggregateRepository problemAggregateRepository,
        IMediator mediator)
    {
        _problemAggregateRepository = problemAggregateRepository;
        _mediator = mediator;
    }

    public async Task<ProblemId> Handle(ConfirmProblemCommand command, CancellationToken cancellationToken)
    {
        var problem = await _problemAggregateRepository.Get(command.ProblemId);
        problem.Confirm();
        await _problemAggregateRepository.SaveAggragate(problem);

        await UpdateReadModel(command.ProblemId, cancellationToken);

        return problem.Id;
    }

    private async Task UpdateReadModel(ProblemId problemId, CancellationToken cancellationToken1)
    {
        await _mediator.Publish(new ProblemUpdated(problemId), cancellationToken1);
    }
}