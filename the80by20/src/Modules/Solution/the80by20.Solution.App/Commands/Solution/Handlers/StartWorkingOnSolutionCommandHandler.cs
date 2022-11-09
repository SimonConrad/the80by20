﻿using MediatR;
using the80by20.Modules.Solution.App.Events.Solution;
using the80by20.Modules.Solution.Domain.Problem.Repositories;
using the80by20.Modules.Solution.Domain.Shared.DomainServices;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.App.Commands.Solution.Handlers;

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

        await UpdateReadModel(solution.Id.Value, cancellationToken);

        return solution.Id.Value;
    }

    public async Task UpdateReadModel(SolutionToProblemId id, CancellationToken ct)
    {
        await _mediator.Publish(new StartedWorkingOnSolution(id), ct);
    }
}