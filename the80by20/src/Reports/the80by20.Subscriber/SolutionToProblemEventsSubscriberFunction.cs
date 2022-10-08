using System;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace the80by20.Subscriber
{
    // INFO https://www.mitrais.com/news-updates/handle-azure-service-bus-messages-queue-using-azure-functions/
    public class SolutionToProblemEventsSubscriberFunction
    {
        [FunctionName("SolutionToProblemEventsSubscriberFunction")]
        public void Run([ServiceBusTrigger("%MessageQueue Name%", Connection = "ServiceBusConnectionString")]string msg, 
        /*Message message,*/ /*MessageReceiver messageReceiver*/ ILogger log)
        {
            try
            {
                //string payload = Encoding.UTF8.GetString(message.Body);
               // log.LogInformation($"C# ServiceBus queue  trigger function processed message: {payload}");

                log.LogInformation($"C# ServiceBus queue  trigger function processed message: {msg}");

                //MyMessageModel model = JsonConvert.DeserializeObject<MyMessageModel>(payload);

                //Do your things here, such as Some Actions or Calls Some Service or Another Method


                //complete the message if there is no error
                //messageReceiver.CompleteAsync(message.SystemProperties.LockToken);

            }
            catch (Exception)
            {
                // Do your error handling here


                // Send message to DeadLetter Queue 
                //messageReceiver.DeadLetterAsync(message.SystemProperties.LockToken);

            }
        }
    }
}
