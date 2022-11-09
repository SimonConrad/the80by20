﻿using MediatR;
using the80by20.Modules.Solution.App.Events.Solution;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.Commands.Solution.Handlers;

[CommandDdd]
public class AddSolutionElementCommandHandler
    : IRequestHandler<AddSolutionElementCommand, SolutionToProblemId>
{
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly IMediator _mediator;

    public AddSolutionElementCommandHandler(
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        IMediator mediator)
    {
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _mediator = mediator;
    }

    public async Task<SolutionToProblemId> Handle(AddSolutionElementCommand command,
        CancellationToken cancellationToken)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(command.SolutionToProblemId);
        solution.AddSolutionElement(command.SolutionElement);
        await _solutionToProblemAggregateRepository.SaveAggragate(solution);

        await UpdateReadModel(solution.Id.Value, cancellationToken);

        return solution.Id.Value;
    }

    public async Task UpdateReadModel(SolutionToProblemId id, CancellationToken ct)
    {
        await _mediator.Publish(new UpdatedSolution(id), ct);
    }
}