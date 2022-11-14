using the80by20.Modules.Solution.Domain.Solution.Entities;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.Domain.Solution.Repositories
{
    /// <summary>
    /// INFO save and restore always via aggregate
    /// INFO  possible aggregate persistance options
    ///  (1) map via raw sql - insert, update, select into aggregate object
    ///  (2) kung-fu with ef fluent mappings
    ///  (3) aggragtes created and restored via its snapshot (dto) which is mapped as simple orm entity; memento design pattern
    ///
    /// Header / Aggregate data (data not used by aggregate invariants), implemented as simple poco class
    /// without relation - but with aggregate-id - loosely coupled, or
    /// with relation - its primary key is set as foreign key referencing aggragte, but without navigation proties from both sites
    /// when we retrieve aggregate from repository aggregate-data class don't not to be retrieved
    /// </summary>
    [AggregateRepositoryDdd]
    [Port]
    public interface ISolutionToProblemAggregateRepository
    {
        Task Create(SolutionToProblemAggregate aggregate);
        Task<SolutionToProblemAggregate> Get(SolutionToProblemId id);
        Task SaveAggragate(SolutionToProblemAggregate aggregate);
        Task<bool> IsTheSolutionAssignedToProblem(ProblemId problemId);
    }
}
