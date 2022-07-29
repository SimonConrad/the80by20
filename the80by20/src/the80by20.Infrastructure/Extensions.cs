using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using the80by20.App.Core.SolutionToProblem.ReadModel;
using the80by20.Domain.SharedKernel;
using the80by20.Infrastructure.Exceptions;
using the80by20.Infrastructure.Time;

namespace the80by20.Infrastructure;

public static class Extensions
{
    public const string OptionsSectionAppName = "app";
    private const string OptionsDataBaseName = "dataBase";


    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.Configure<AppOptions>(configuration.GetRequiredSection("app"));
        services.AddSingleton<ExceptionMiddleware>();
        //services.AddHttpContextAccessor();

        // todo
        services
            .AddDatabase(configuration)
            .AddSingleton<IClock, Clock>();;
        
        //services.AddCustomLogging();
        //services.AddSecurity();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            //swagger.EnableAnnotations();
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "the-80-by-20 API",
                Version = "v1"
            });
        });

        services.AddMediatR(typeof(SolutionToProblemReadModelEventHandler));

        //services.AddAuth(configuration);

        return services;
    }

    public static async Task<WebApplication> UseInfrastructure(this WebApplication app, IConfiguration configuration)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "The 80 by 20"));

        //todo
        //app.UseReDoc(reDoc =>
        //{
        //    reDoc.RoutePrefix = "docs";
        //    reDoc.SpecUrl("/swagger/v1/swagger.json");
        //    reDoc.DocumentTitle = "MySpot API";
        //});
        //app.UseAuthentication();
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();


        return app;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}