using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using the80by20.App.Core.SolutionToProblem.CommandsHandlers.ProblemHandlers;
using the80by20.App.Core.SolutionToProblem.ReadModel;
using the80by20.Domain.SharedKernel;
using the80by20.Infrastructure.Exceptions;
using the80by20.Infrastructure.HandlersDecorators;
using the80by20.Infrastructure.Security;
using the80by20.Infrastructure.Security.Adapters;
using the80by20.Infrastructure.Security.Adapters.Auth;
using the80by20.Infrastructure.Security.Adapters.Security;
using the80by20.Infrastructure.Security.Adapters.Users;
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
        services.AddHttpContextAccessor();

        services.AddDal(configuration);

        services.AddSingleton<IClock, Clock>();
        
        //services.AddCustomLogging(); // todo
        
        services.AddEndpointsApiExplorer(); // todo

        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "The 80 by 20",
                Description = "system created from model to implementaion; model done used event storming, implemntaion done with ddd (strategic, tactical), architectural patterns based on modules drivers; high cohesion low coupling; tests; infrastructure"
            });
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description =
                    "Enter 'Bearer' [space] and then your valid token in the text input below." +
                    "\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        services.AddValidatorsFromAssemblyContaining<CreateProblemValidator>();

        services.AddMediatR(typeof(SolutionToProblemReadModelEventHandler), typeof(GetUserQueryHandler));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        
       
        //services.Scan(s => s.FromAssemblies(infrastructureAssembly)
        //    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
        //    .AsImplementedInterfaces()
        //    .WithScopedLifetime());

        services.AddAuth(configuration);
        services.AddSecurity();

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