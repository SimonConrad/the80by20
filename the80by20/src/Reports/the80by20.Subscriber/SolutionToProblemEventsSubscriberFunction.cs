using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace the80by20.Subscriber
{
    // INFO https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-service-bus-trigger?tabs=in-process%2Cextensionv5
    public class SolutionToProblemEventsSubscriberFunction
    {
        [FunctionName("SolutionToProblemEventsSubscriberFunction")]
        public void Run([ServiceBusTrigger("%MessageQueue Name%", Connection = "ServiceBusConnectionString")]string myQueueItem,
            Int32 deliveryCount,
            DateTime enqueuedTimeUtc,
            string messageId,
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            log.LogInformation($"EnqueuedTimeUtc={enqueuedTimeUtc}");
            log.LogInformation($"DeliveryCount={deliveryCount}");
            log.LogInformation($"MessageId={messageId}");
        }
    }
}
