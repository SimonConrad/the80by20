using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.TacticalDDD;

namespace Core.Domain.SolutionToProblem
{
    // TODO think if data encapuslation is needed
    // TODO mapping by DAL strategy
    [AggregateDataDdd]
    public class SolutionToProblemData
    {
        public SolutionToProblemId AggregateId { get; set; }

        public Guid UserId { get; set; }

        public string Description { get; set; }


        public string Category { get; set; }
    }
}
