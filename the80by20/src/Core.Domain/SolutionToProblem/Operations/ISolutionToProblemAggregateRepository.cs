using Common.DDD;

namespace Core.Domain.SolutionToProblem.Operations
{
    // INFO save and restore always via aggregate
    [AggregateRepositoryDdd]
    public interface ISolutionToProblemAggregateRepository // todo implement in Cire.Dal - create new layer
    {
        Task CreateProblem(SolutionToProblemAggregate aggregate, SolutionToProblemData data);

        // INFO  possible aggregate persistance options
        // (1) map via raw sql - insert, update, select into aggregate object
        // (2) kung-fu qith ef fluent mappings
        // (3) aggragtes creates and constructs via its snapshot (dto) which is mapped as simple orm entity; memento design pattern
        // Header / Aggregate data (not included in iaggragtes invariants) as simple poco class with retaltion - its primary key is set as foreign key referencing aggragtae. When we retrieve aggrate from repository aggregate-data class don not to be retrieved
    }
}
