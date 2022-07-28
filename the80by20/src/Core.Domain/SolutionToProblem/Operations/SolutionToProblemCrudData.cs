using Common.DDD;

namespace Core.Domain.SolutionToProblem.Operations
{
    /// <summary>
    /// it is not completely separate from aggregate, like anemic entity
    /// </summary>
    [AggregateDataDdd]
    public class SolutionToProblemCrudData : AggergateData
    {
        public Guid UserId { get; private set; }

        public string Description { get; set; }
        
        public string DescriptionLinks { get; set; }

        // TODO Create VO Category
        public string Category { get; set; }

        public void SetUser(Guid userId)
        {
            UserId = userId;
        }
    }
}
