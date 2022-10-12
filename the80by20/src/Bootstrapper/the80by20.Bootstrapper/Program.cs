using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using the80by20.Masterdata.Infrastructure;
using the80by20.Solution.App;
using the80by20.Solution.Domain;
using the80by20.Solution.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddMasterDataDal(builder.Configuration);

ConfigureLogging(builder);

var app = builder.Build();

app.UseInfrastructure(builder.Configuration);

app.MapGet("api", (IOptions<AppOptions> options) => Results.Ok(options.Value.Name));

app.Run();

void ConfigureLogging(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Host.UseSerilog((context, loggerConfiguration) =>
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
            .WriteTo.File(webApplicationBuilder.Configuration.GetValue<string>("LogFilePath"),
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
        // .Seq("http://localhost:5341"); //todo kibana, pod�aczy� appinsights
    });
}