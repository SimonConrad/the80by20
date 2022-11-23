using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Convey;
using Convey.MessageBrokers.RabbitMQ;
using the80by20.Shared.Abstractions.Contexts;
using the80by20.Shared.Abstractions.Modules;
using the80by20.Shared.Abstractions.Time;
using the80by20.Shared.Infrastucture.Api;
using the80by20.Shared.Infrastucture.Auth;
using the80by20.Shared.Infrastucture.Commands;
using the80by20.Shared.Infrastucture.Context;
using the80by20.Shared.Infrastucture.Decorators;
using the80by20.Shared.Infrastucture.Events;
using the80by20.Shared.Infrastucture.Exceptions;
using the80by20.Shared.Infrastucture.Kernel;
using the80by20.Shared.Infrastucture.Messaging;
using the80by20.Shared.Infrastucture.Modules;
using the80by20.Shared.Infrastucture.Queries;
using the80by20.Shared.Infrastucture.Services;
using the80by20.Shared.Infrastucture.SqlServer;
using the80by20.Shared.Infrastucture.Time;

namespace the80by20.Shared.Infrastucture
{
    // TODO compare with Confab.Shared.Infrastructure
    internal static class Extensions
    {
        private const string CorsPolicy = "cors";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
            IConfiguration configuration,
            IList<Assembly> assemblies,
            IList<IModule> modules)
        {

            var disabledModules = new List<string>();
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var conf = serviceProvider.GetRequiredService<IConfiguration>();
                foreach (var (key, value) in conf.AsEnumerable())
                {
                    if (!key.Contains(":module:enabled"))
                    {
                        continue;
                    }

                    if (!bool.Parse(value))
                    {
                        disabledModules.Add(key.Split(":")[0]);
                    }
                }
            }

            AddCors(services, configuration);
            AddSwagger(services);

            // todo move to extensions in infrastruture/context
            services.AddSingleton<IContextFactory, ContextFactory>();
            services.AddHttpContextAccessor();
            services.AddTransient<IContext>(sp => sp.GetRequiredService<IContextFactory>().Create());

            services.AddModuleInfo(modules);
            services.AddModuleRequests(assemblies);
            services.AddAuth(modules);
            services.AddErrorHandling();
            services.AddCommands(assemblies);
            services.AddQueries(assemblies);
            services.AddDomainEvents(assemblies);
            services.AddEvents(assemblies);
            services.AddMessaging();
            services.AddSqlServer();
            services.AddTransactionalDecorators();
            services.AddCommandHandlersDecorators();
            services.AddSingleton<IClock, Clock>();
            services.AddHostedService<AppInitializer>();
            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    var removedParts = new List<ApplicationPart>();
                    foreach (var disabledModule in disabledModules)
                    {
                        var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule,
                            StringComparison.InvariantCultureIgnoreCase));
                        removedParts.AddRange(parts);
                    }

                    foreach (var part in removedParts)
                    {
                        manager.ApplicationParts.Remove(part);
                    }

                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });

            services.Configure<AppOptions>(configuration.GetRequiredSection(key: "app"));

            // info rabbitmq
            services
                .AddConvey()
                .AddRabbitMq()
                .Build();
            
            return services;
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.CustomSchemaIds(x => x.FullName);

                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "The 80 by 20",
                    Version = "v1",
                    Description = "system created from model to implementaion; " +
                    "model done used event storming, " +
                    "implemntaion done with ddd (strategic, tactical), architectural patterns based on each modules architectural drivers;" +
                    "high cohesion low coupling; " +
                    "tests; " +
                    "infrastructure"
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
            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy(CorsPolicy, x =>
                {
                    x.WithOrigins(appOptions.FrontUrl) // info to allow all: .WithOrigins("*")
                     .WithMethods("POST", "PUT", "DELETE")
                     .WithHeaders("Content-Type", "Authorization");
                });
            });
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseCors(CorsPolicy);
            app.UseErrorHandling();
            app.UseSwagger();
            //app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "The 80 by 20"));
            app.UseReDoc(reDoc =>
            {
                reDoc.RoutePrefix = "docs";
                reDoc.SpecUrl("/swagger/v1/swagger.json");
                reDoc.DocumentTitle = "Confab API";
            });
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

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

        public static string GetModuleName(this object value)
           => value?.GetType().GetModuleName() ?? string.Empty;

        public static string GetModuleName(this Type type)
        {
            if (type?.Namespace is null)
            {
                return string.Empty;
            }

            return type.Namespace.StartsWith("the80by20.Modules.")
                ? type.Namespace.Split(".")[2].ToLowerInvariant()
                : string.Empty;
        }

    }
}
