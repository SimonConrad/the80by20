using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Runtime.CompilerServices;
using the80by20.Shared.Abstractions.Time;
using the80by20.Shared.Infrastucture.Api;
using the80by20.Shared.Infrastucture.Exceptions;
using the80by20.Shared.Infrastucture.Services;
using the80by20.Shared.Infrastucture.SqlServer;
using the80by20.Shared.Infrastucture.Time;

[assembly: InternalsVisibleTo("the80by20.Bootstrapper")]
[assembly: InternalsVisibleTo("the80by20.Tests.Integration")]
namespace the80by20.Shared.Infrastucture
{
    // TODO compare with Confab.Shared.Infrastructure
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddErrorHandling();

            services.AddSingleton<IClock, Clock>();

            services.AddHostedService<AppInitializer>();

            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });

            //var appOptions = configuration.GetOptions<AppOptions>(sectionName:"app");
            //services.AddSingleton(appOptions);
            services.Configure<AppOptions>(configuration.GetRequiredSection(key: "app"));

            services.AddHttpContextAccessor();

            services.AddSqlServer();

            services.AddEndpointsApiExplorer(); // todo what for?

            AddSwagger(services);

            AddCors(services, configuration);

            return services;
        }

        private static void AddSwagger(IServiceCollection services)
        {
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
        }

        private static void AddCors(IServiceCollection services, IConfiguration configuration)
        {
            var appOptions = configuration.GetOptions<AppOptions>("app");
            // INFO CORS configuration based on ms-docs: https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0
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
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseErrorHandling();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "The 80 by 20"));

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("api", (IOptions<AppOptions> options) => Results.Ok(options.Value.Name));
            });

            return app;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }

        public static void AddLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, loggerConfiguration) =>
            {
                // todo make  serilog read from appseettings.ENV.json so that logging levels are overriden by this what we have in appseetings (based on application environment)
                loggerConfiguration
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information);
                //"Microsoft.EntityFrameworkCore.Database.Command": "Warning",
                //"Microsoft.EntityFrameworkCore.Infrastructure": "Warning"

                loggerConfiguration
                    .WriteTo
                    .Console();
                //info can use outputTemplate, like below

                loggerConfiguration
                    .Enrich.FromLogContext()
                    .WriteTo.File(builder.Configuration.GetValue<string>("LogFilePath"),
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        retainedFileCountLimit: 10,
                        fileSizeLimitBytes: 4194304, // 4MB
                                                     //restrictedToMinimumLevel: LogEventLevel.Information,
                        outputTemplate:
                        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] ({SourceContext}.{Method}) {Message}{NewLine}{Exception}");

                // todo .{Method} is not logging method name
                //loggerConfiguration.ReadFrom.Configuration(builder.Configuration);

                // .WriteTo
                // .File("logs.txt")
                // .WriteTo
                // .Seq("http://localhost:5341"); //todo kibana and appinsights
            });
        }
    }
}
