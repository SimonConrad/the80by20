using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using the80by20.Modules.Sale.App.Clients.Solution;
using the80by20.Modules.Sale.App.Clients.Solution.DTO;

namespace the80by20.Modules.Sale.Infrastructure.Clients
{
    internal class SolutionApiClient : ISolutionApiClient
    {
        public Task<SolutionToProblemDto> GetSolutionToProblemDto(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
