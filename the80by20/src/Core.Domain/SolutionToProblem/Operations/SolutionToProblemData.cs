using Common.DDD;
using Core.Domain.SolutionToProblem.Capabilities;

namespace Core.Domain.SolutionToProblem.Operations
{
    // TODO think if data encapuslation is needed
    // TODO mapping by DAL strategy
    [AggregateDataDdd]
    public class SolutionToProblemData : BaseEntityData
    {
        public Guid UserId { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }
    }
}
