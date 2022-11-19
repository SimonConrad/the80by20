using the80by20.Modules.Solution.App.Problem.Events;
using the80by20.Modules.Solution.Domain.Problem.Repositories;
using the80by20.Modules.Solution.Domain.Shared.DomainServices;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.Commands;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Solution.App.Problem.Commands.Handlers;

public class RejectProblemCommandHandler : ICommandHandler<RejectProblemCommand>
{
    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly ProblemRejectionDomainService _problemRejectionDomainService;
    private readonly IEventDispatcher _eventDispatcher;

    public RejectProblemCommandHandler(
        IProblemAggregateRepository problemAggregateRepository,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        ProblemRejectionDomainService problemRejectionDomainService,
        IEventDispatcher eventDispatcher)
    {
        _problemAggregateRepository = problemAggregateRepository;
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _problemRejectionDomainService = problemRejectionDomainService;
        _eventDispatcher = eventDispatcher;
    }
    public async Task HandleAsync(RejectProblemCommand command)
    {
        var problem = await _problemAggregateRepository.Get(command.ProblemId);
        problem = await _problemRejectionDomainService.RejectProblem(problem, _solutionToProblemAggregateRepository);
        await _problemAggregateRepository.SaveAggragate(problem);

        await _eventDispatcher.PublishAsync(new ProblemUpdated(command.ProblemId, problem, null));
    }
}