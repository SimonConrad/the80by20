using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.Domain.Core.SolutionToProblem.Operations.Problem
{
    /// <summary>
    /// it is not completely separate from aggregate, like anemic entity
    /// </summary>
    [AggregateDataDdd]
    [EntityDdd]
    public class ProblemCrudData : AggergateData
    {
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
    }
}
