using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using System.Runtime.CompilerServices;
using the80by20.Shared.Infrastucture.Exceptions;


[assembly: InternalsVisibleTo("the80by20.Bootstrapper")]
namespace the80by20.Shared.Infrastucture
{
    internal static class ExtensionsApplicationBuilder
    {
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
