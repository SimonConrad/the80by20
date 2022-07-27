using Core.App.SolutionToProblem.ReadModel;
using Core.Infrastructure.DAL;
using MediatR;
using Microsoft.OpenApi.Models;

namespace Core.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services.Configure<AppOptions>(configuration.GetRequiredSection("app"));

        //services.AddSingleton<ExceptionMiddleware>();
        //services.AddHttpContextAccessor();

        // todo
        services.AddDatabase(configuration);
        
        //todo
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "The 80 by 20", Version = "v1" });
        });

        // todo
        services.AddMediatR(typeof(SolutionToProblemReadModelHandler));

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        // todo
        //app.UseMiddleware<ExceptionMiddleware>();
        
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
        
        app.UseHttpsRedirection();

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