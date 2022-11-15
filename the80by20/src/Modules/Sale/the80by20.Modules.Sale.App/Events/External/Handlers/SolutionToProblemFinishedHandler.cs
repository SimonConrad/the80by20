using Microsoft.Extensions.Logging;
using the80by20.Shared.Abstractions.Events;

namespace the80by20.Modules.Sale.App.Solution.Events.External.Handlers
{
    internal class SolutionToProblemFinishedHandler : IEventHandler<SolutionToProblemFinished>
    {
        private readonly ILogger<SolutionToProblemFinishedHandler> _logger;

        public SolutionToProblemFinishedHandler(ILogger<SolutionToProblemFinishedHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(SolutionToProblemFinished @event)
        {
            // INFO
            // create product, can be with same id as solution, caouse problem becomes solution becomes product
            // send user who created problem notification taht he / she
            // can start the order - add product to the order
            // order is the agregate?


            // TODO
            // INFO
            // archive the problem , archive solution, create product via event send to sale module - objects lifecycle;
            // same id in all 3 candidate for saga / process manager
            // send event solution-to-problem-finished to module sale

            // data: prolem / solution becomes product with informations: SolutionSummary, SolutionElements, Price, additional infos from aggregates: problem and solutio
            // behaviors: user can buy it
            // invariants: ?
            // another aggregate / entity - order - event storming

            //await Task.Delay(10_000); // INFO Background dispatcher is used so publisher of the event will no wai
            _logger.LogInformation($"Product created {@event.solutionId}");
        }
    }
}