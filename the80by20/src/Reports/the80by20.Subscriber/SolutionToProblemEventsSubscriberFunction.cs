using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace the80by20.Reports.Subscriber
{
    // INFO https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-service-bus-trigger?tabs=in-process%2Cextensionv5
    public class SolutionToProblemEventsSubscriberFunction
    {
        [FunctionName("SolutionToProblemEventsSubscriberFunction")]
        public void Run([ServiceBusTrigger("%MessageQueue Name%", Connection = "ServiceBusConnectionString")] string message,
            int deliveryCount,
            DateTime enqueuedTimeUtc,
            string messageId,
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");
            log.LogInformation($"EnqueuedTimeUtc={enqueuedTimeUtc}");
            log.LogInformation($"DeliveryCount={deliveryCount}");
            log.LogInformation($"MessageId={messageId}");


            // TODO deserialize event (message function parameter) into model, persist it in denormalized table,
            // table is inside dedicated db schema but same database for whole sytem,
            // reports module has reading part - web api
            // this table will be for querying most popular problem categories report (analytic readmodel)
        }
    }
}
