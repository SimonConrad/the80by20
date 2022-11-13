using MediatR;
using the80by20.Modules.Solution.App.Solution.Events;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Solution.App.ReadModel;

/// <summary>
/// TODO: in production rather do alternative to this read model handling problems / solutions updates mechanism
/// is just use readmodel queries that do  projection (returns SolutionToProblemReadModel) composed from data retrieved from
/// aggregate repos and administration category crud
/// </summary>
/// 

// INFO
// Dedicated read model storing data in unnormalized table optimized for fast reads
// it is not immediatly consistent with aggregata data but eventually consistent
[ReadModelDdd]
public class SolutionReadModelHandler :
    INotificationHandler<StartedWorkingOnSolution>,
    INotificationHandler<UpdatedSolution>
{
    private readonly ISolutionToProblemReadModelUpdates _readModelUpdates;
    private readonly ISolutionToProblemReadModelQueries _readModelQueries;
    private readonly ISolutionToProblemAggregateRepository _solutionToProblemAggregateRepository;

    public SolutionReadModelHandler(ISolutionToProblemReadModelUpdates readModelUpdates,
        ISolutionToProblemReadModelQueries readModelQueries,
        ISolutionToProblemAggregateRepository solutionToProblemAggregateRepository)
    {
        _readModelUpdates = readModelUpdates;
        _readModelQueries = readModelQueries;
        _solutionToProblemAggregateRepository = solutionToProblemAggregateRepository;
    }

    public async Task Handle(StartedWorkingOnSolution @event, CancellationToken cancellationToken)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(@event.SolutionToProblemId);

        var rm = await _readModelQueries.GetByProblemId(solution.ProblemId);

        rm.SolutionToProblemId = solution.Id;
        rm.Price = solution.Price;
        rm.SolutionSummary = solution.SolutionSummary.Content;
        rm.SolutionElements = solution.SolutionElements.ToSnapshotInJson();
        rm.WorkingOnSolutionEnded = solution.WorkingOnSolutionEnded;

        await _readModelUpdates.Update(rm);
    }

    public async Task Handle(UpdatedSolution @event, CancellationToken cancellationToken)
    {
        var solution = await _solutionToProblemAggregateRepository.Get(@event.SolutionToProblemId);

        var rm = await _readModelQueries.GetBySolutionId(solution.Id.Value);

        rm.Price = solution.Price;
        rm.SolutionSummary = solution.SolutionSummary.Content;
        rm.SolutionElements = solution.SolutionElements.ToSnapshotInJson();
        rm.WorkingOnSolutionEnded = solution.WorkingOnSolutionEnded;

        await _readModelUpdates.Update(rm);
    }
}
