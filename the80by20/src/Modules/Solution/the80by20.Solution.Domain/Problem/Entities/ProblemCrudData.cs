using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel;

namespace the80by20.Modules.Solution.Domain.Problem.Entities
{
    // INFO
    // maybe *Description* information should be directly inside ProblemAggregate as there is invariant that "problem cannot exists without description"
    // putting description inside problem-aggregate guards against unconsistent state in which problem is peristsed without description
    // 
    // maybe go furteher and transform ProblemCrudData to value object and put inside SolutionToProblemAggregate

    // TODO refactor to above

    /// <summary>
    /// it is not completely separate from aggregate, like anemic entity
    /// </summary>
    [AggregateDataDdd]
    [EntityDdd]
    public class ProblemCrudData : AggergateData
    {
        // INFO maybe add user as separate entity in this module
        // it will have informations and behaviors important in context of this module
        // id can have same value as in users-module (snowflake id )
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Description { get; private set; }
        public Guid Category { get; private set; }

        public ProblemCrudData(Guid aggregateId,
            Guid userId,
            DateTime createdAt,
            string description,
            Guid category)
        {
            AggregateId = aggregateId;
            UserId = userId;
            CreatedAt = createdAt;
            Description = description;
            Category = category;
        }

        public void Update(string description, Guid category)
        {
            Description = description;
            Category = category;
        }
    }
}
