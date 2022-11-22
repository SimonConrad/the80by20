using the80by20.Services.Sale.App.Clients.Solution;
using the80by20.Services.Sale.App.Clients.Solution.DTO;
using the80by20.Services.Sale.Infrastructure.Clients.Requests;
using the80by20.Shared.Abstractions.Modules;

namespace the80by20.Services.Sale.Infrastructure.Clients
{
    internal class SolutionApiClient : ISolutionApiClient
    {
        private readonly IModuleClient _moduleClient;

        public SolutionApiClient(IModuleClient moduleClient) // INFO if need to call external service instead of IModuleClient us IHttpClientFactory
        {
            _moduleClient = moduleClient;
        }

        public Task<SolutionToProblemDto> GetSolutionToProblemDto(Guid id)
            => _moduleClient.SendAsync<SolutionToProblemDto>("solutions/get",
                new GetSolutionToProblem
                {
                    SolutionToProblemId = id
                });
    }
}
