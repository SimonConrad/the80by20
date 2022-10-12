using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using the80by20.Shared.Abstractions.AppLayer;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel;
using the80by20.Shared.Infrastucture.Time;
using the80by20.Solution.App.Security.Commands.Handlers;
using the80by20.Solution.App.SolutionToProblem.ReadModel;
using the80by20.Solution.Infrastructure.Exceptions;
using the80by20.Solution.Infrastructure.InputValidation;
using the80by20.Solution.Infrastructure.Logging;
using the80by20.Solution.Infrastructure.Security.Adapters.Auth;
using the80by20.Solution.Infrastructure.Security.Adapters.Security;
using the80by20.Solution.Infrastructure.Security.Adapters.Users;

namespace the80by20.Solution.Infrastructure
{
    public static class Extensions
    {
        public const string OptionsSectionAppName = "app";
        private const string OptionsDataBaseName = "dataBase";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            //var appOptions = configuration.GetOptions<AppOptions>(sectionName:"app");
            //services.AddSingleton(appOptions);

            services.Configure<AppOptions>(configuration.GetRequiredSection(key: "app"));

            services.AddSingleton<ExceptionMiddleware>();
            services.AddHttpContextAccessor();

            services.AddDal(configuration);

            services.AddSingleton<IClock, Clock>();

            services.AddCustomLogging();
            services.AddSecurity();
            services.AddInputValidation();
            services.AddValidatorsFromAssemblyContaining<SignUpInputValidator>();

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

            AddMediatRStaff(services);

            var infrastructureAssembly = typeof(AppOptions).Assembly;

            services.Scan(s => s.FromAssemblies(infrastructureAssembly)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());


            services.AddAuth(configuration);

            var appOptions = configuration.GetOptions<AppOptions>("app");

            // INFO Do cors in proper way https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0
            // Default Policy
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(appOptions.FrontUrl)
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });


            return services;
        }

        private static void AddMediatRStaff(IServiceCollection services)
        {
            // todo w zwi¹zku z tym, ¿e mediator mocno pl¹cze koncpet query i command, nie sa on oddzielone lepiej chyba przpi¹c siê na rozwi¹zanie od devmentors np te w mysport
            // albo rozdzielic w ramach mediar jako: https://cezarypiatek.github.io/post/why-i-dont-use-mediatr-for-cqrs/
            services.AddMediatR(typeof(SolutionToProblemReadModelEventHandler), typeof(GetUserHandler));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            // info more problems with this then pozytku services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        }

        public static WebApplication UseInfrastructure(this WebApplication app, IConfiguration configuration)
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


            app.UseCors();


            return app;
        }

        // TODO daæ w jedno miejsce
        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetRequiredSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}