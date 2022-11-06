using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using the80by20.Shared.Abstractions.Messaging;
using the80by20.Shared.Infrastucture.Messaging.Brokers;

namespace the80by20.Shared.Infrastucture.Messaging
{
    internal static class Extensions
    {
        private const string SectionName = "messaging";

        internal static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBroker, MessageBroker>();
            services.AddSingleton<IMessageChannel, MessageChannel>();
            services.AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>();

            var messagingOptions = services.GetOptions<MessagingOptions>(SectionName);
            services.AddSingleton(messagingOptions);

            if (messagingOptions.UseBackgroundDispatcher)
            {
                services.AddHostedService<BackgroundDispatcher>();
            }

            return services;
        }
    }
}
