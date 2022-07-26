using Common.DDD;
using Core.Domain.SolutionToProblem.Capabilities;

namespace Core.Domain.SolutionToProblem.Operations
{
    [AggregateDataDdd]
    public class SolutionToProblemData : AggergateData
    {
        public Guid UserId { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }
    }
}
