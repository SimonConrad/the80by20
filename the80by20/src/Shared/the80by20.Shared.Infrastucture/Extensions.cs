﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Runtime.CompilerServices;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel;
using the80by20.Shared.Infrastucture.Configuration;
using the80by20.Shared.Infrastucture.Exceptions;
using the80by20.Shared.Infrastucture.Time;

[assembly: InternalsVisibleTo("the80by20.Bootstrapper")]
namespace the80by20.Shared.Infrastucture
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            //var appOptions = configuration.GetOptions<AppOptions>(sectionName:"app");
            //services.AddSingleton(appOptions);

            services.Configure<AppOptions>(configuration.GetRequiredSection(key: "app"));

            services.AddSingleton<ExceptionMiddleware>();
            services.AddHttpContextAccessor();

            services.AddSingleton<IClock, Clock>();

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

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration configuration)
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
            

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("api", (IOptions<AppOptions> options) => Results.Ok(options.Value.Name));
            });

            app.UseCors();

            return app;
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
                // .Seq("http://localhost:5341"); //todo kibana, podłaczyć appinsights
            });
        }
    }
}
