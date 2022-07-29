using the80by20.Domain.ArchitectureBuildingBlocks;

namespace the80by20.Domain.Core.SolutionToProblem.Operations
{
    /// <summary>
    /// it is not completely separate from aggregate, like anemic entity
    /// </summary>
    [AggregateDataDdd]
    [EntityDdd]
    public class SolutionToProblemCrudData : AggergateData
    {
        public Guid UserId { get; private set; }

        public string Description { get; set; }
        
        public string DescriptionLinks { get; set; }

        // TODO Create VO Category
        public Guid Category { get; set; }

        public void SetUser(Guid userId)
        {
            UserId = userId;
        }
    }
}
