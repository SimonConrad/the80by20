using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using the80by20.Shared.Abstractions.Messaging;
using the80by20.Shared.Abstractions.Modules;

namespace the80by20.Shared.Infrastucture.Messaging.Brokers
{
    internal class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient _moduleclient;

        public InMemoryMessageBroker(IModuleClient moduleclient)
        {
            _moduleclient = moduleclient;
        }

        public async Task PublishAsync(params IMessage[] messages)
        {
            if (messages is null)
            {
                return;
            }

            messages = messages.Where(x => x is not null).ToArray();

            if (!messages.Any())
            {
                return;
            }

            var tasks = new List<Task>();

            foreach (var message in messages)
            {
                tasks.Add(_moduleclient.PublishAsync(message));
            }

            await Task.WhenAll(tasks);
        }
    }
}
