using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel;

namespace the80by20.Solution.Domain.Operations.Problem
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

        public void Update(string description, Guid category)
        {
            Description = description;
            Category = category;
        }
    }
}
