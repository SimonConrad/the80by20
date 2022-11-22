using Convey;
using Convey.CQRS.Events;
using Convey.MessageBrokers.RabbitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Services.Sale.App.Clients.Solution;
using the80by20.Services.Sale.Infrastructure.Clients;
using the80by20.Shared.Abstractions.Time;
using the80by20.Shared.Infrastucture.Auth;
using the80by20.Shared.Infrastucture.Commands;
using the80by20.Shared.Infrastucture.Context;
using the80by20.Shared.Infrastucture.Events;
using the80by20.Shared.Infrastucture.Messaging;
using the80by20.Shared.Infrastucture.Modules;
using the80by20.Shared.Infrastucture.Queries;
using the80by20.Shared.Infrastucture.Services;
using the80by20.Shared.Infrastucture.Time;

namespace the80by20.Services.Sale.Infrastructure
{
    public static class Extensions
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddAuth();
            services.AddCommands(assemblies);
            services.AddQueries(assemblies);
            services.AddEvents(assemblies);
            services.AddSingleton<IClock, Clock>();
            services.AddMessaging();
            services.AddModuleRequests(assemblies);
            services.AddMemoryCache();
            services.AddSingleton<IContextFactory, ContextFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(sp => sp.GetRequiredService<IContextFactory>().Create());
            services.AddHostedService<AppInitializer>();
            
            //todo rabbitmq
            // services
            //     .AddConvey()
            //     .AddRabbitMq()
            //     .AddEventHandlers()
            //     .AddInMemoryEventDispatcher()
            //     .Build();
            //
            
            services
                .AddSingleton<ISolutionApiClient, SolutionApiClient>();
            

            return services;
        }
    }
}
 