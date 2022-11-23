using Convey;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Microsoft.Extensions.Options;
using the80by20.Services.Sale.App.Events.External;
using the80by20.Services.Sale.Infrastructure;
using the80by20.Shared.Infrastucture;

namespace the80by20.Services.Sale.Api;

public static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var configuration = builder.Configuration;
        var env = builder.Environment;

        AddServices(builder);

        WebApplication app = builder.Build();


        UseMiddlewares(env, app);

        await app.RunAsync();
    }

    private static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddInfrastructure();
    }

    private static void UseMiddlewares(IWebHostEnvironment env, WebApplication app)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        
        // info rabbitmq
        app.UseConvey();
        app.UseRabbitMq()
            .SubscribeEvent<SolutionFinished>();

    }
}

