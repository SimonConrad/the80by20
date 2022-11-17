using the80by20.Modules.Solution.App.Solution.Events;
using the80by20.Modules.Solution.Domain.Solution.Repositories;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Solution.App.ReadModel;

// INFO
// Dedicated read model storing data in unnormalized table optimized for fast reads
// it is not immediatly consistent with aggregata data but eventually consistent
[ReadModelDdd]
public class SolutionReadModelHandler :
    IEventHandler<StartedWorkingOnSolution>,
    IEventHandler<UpdatedSolution>
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

    public async Task HandleAsync(StartedWorkingOnSolution @event)
    {
        var solution = @event.solution;

        var rm = await _readModelQueries.GetByProblemId(solution.ProblemId);

        rm.SolutionToProblemId = solution.Id;
        rm.Price = solution.Price;
        rm.SolutionSummary = solution.SolutionSummary.Content;
        rm.SolutionElements = solution.SolutionElements.ToSnapshotInJson();
        rm.WorkingOnSolutionEnded = solution.WorkingOnSolutionEnded;

        await _readModelUpdates.Update(rm);
    }

    public async Task HandleAsync(UpdatedSolution @event)
    {
        var solution = @event.solution;

        var rm = await _readModelQueries.GetBySolutionId(solution.Id.Value);

        rm.Price = solution.Price;
        rm.SolutionSummary = solution.SolutionSummary.Content;
        rm.SolutionElements = solution.SolutionElements.ToSnapshotInJson();
        rm.WorkingOnSolutionEnded = solution.WorkingOnSolutionEnded;

        await _readModelUpdates.Update(rm);
    }
}
