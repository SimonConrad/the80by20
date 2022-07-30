using MediatR;
using Microsoft.Extensions.DependencyInjection;
using the80by20.App.Core.SolutionToProblem.Events;
using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Operations.DomainServices;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;

namespace the80by20.App.Core.SolutionToProblem.Commands.ProblemHandlers;

[CommandDdd]
public class RejectProblemCommandHandler : IRequestHandler<RejectProblemCommand, ProblemId>
{
    private readonly IProblemAggregateRepository _problemAggregateRepository;
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;
    private readonly ProblemRejectionDomainService _problemRejectionDomainService;
    private readonly IServiceScopeFactory _servicesScopeFactory;

    public RejectProblemCommandHandler(
        IProblemAggregateRepository problemAggregateRepository,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository,
        ProblemRejectionDomainService problemRejectionDomainService,
        IServiceScopeFactory servicesScopeFactory)
    {
        _problemAggregateRepository = problemAggregateRepository;
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
        _problemRejectionDomainService = problemRejectionDomainService;
        _servicesScopeFactory = servicesScopeFactory;
    }
    public async Task<ProblemId> Handle(RejectProblemCommand command, CancellationToken cancellationToken)
    {
        var problem = await _problemAggregateRepository.Get(command.ProblemId);
        problem = await _problemRejectionDomainService.RejectProblem(problem, _solutionToProblemAggregateRepository);
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