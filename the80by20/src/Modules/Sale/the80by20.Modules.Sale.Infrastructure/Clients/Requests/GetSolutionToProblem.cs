using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using the80by20.Shared.Abstractions.Queries;

namespace the80by20.Modules.Sale.Infrastructure.Clients.Requests
{
    public class GetSolutionToProblem
    {
        public Guid SolutionToProblemId { get; set; }
    }
}
