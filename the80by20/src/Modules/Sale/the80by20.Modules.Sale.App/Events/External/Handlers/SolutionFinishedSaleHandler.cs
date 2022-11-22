using Microsoft.Extensions.Logging;
using the80by20.Modules.Sale.App.Clients.Solution;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Sale.App.Events.External.Handlers
{
    internal class SolutionFinishedSaleHandler : IEventHandler<SolutionFinished>
    {
        private readonly ILogger<SolutionFinishedSaleHandler> _logger;
        private readonly ISolutionApiClient _solutionApiClient;

        public SolutionFinishedSaleHandler(ILogger<SolutionFinishedSaleHandler> logger,
            ISolutionApiClient solutionApiClient)
        {
            _logger = logger;
            _solutionApiClient = solutionApiClient;
        }

        public async Task HandleAsync(SolutionFinished @event)
        {
            // info create product based on solution, becomes perspective
            // send event ProductCreated
            _logger.LogInformation($"{nameof(ProductCreated)} {@event.solutionId}");
            // INFO
            // publish event ProductCreated which starts Saga InitializeSaleSaga
            
            // INFO
            // create product, can be with same id as solution, caouse problem becomes solution becomes product
            // send user who created problem notification taht he / she
            // can start the order - add product to the order
            // order is the agregate?

            // todo
            // get more data using ISolutionApiClient

            var details = await _solutionApiClient.GetSolutionToProblemDto(@event.solutionId);


            // data: prolem / solution becomes product with informations: SolutionSummary, SolutionElements, Price, additional infos from aggregates: problem and solutio
            // behaviors: user can buy it
            // invariants: ?
            // another aggregate / entity - order - event storming

            // INFO
            // Background dispatcher is used so publisher of the event will no wait
            // can be checked by: await Task.Delay(10_000); 


            _logger.LogInformation($"Product created {@event.solutionId}");
            // INFO
            // publish event ProductCreated which starts Saga InitializeSaleSaga
        }
    }
}