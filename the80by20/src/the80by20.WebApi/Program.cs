
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using the80by20.App;
using the80by20.Domain;
using the80by20.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);


builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    //todo make working thath serilog read from logging levels are overriden by this what we have in appseetings (this is choden by environment)
    // todo configure logger to read from appsettings.json
    loggerConfiguration
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information);
    //"Microsoft.EntityFrameworkCore.Database.Command": "Warning",
    //"Microsoft.EntityFrameworkCore.Infrastructure": "Warning"

    loggerConfiguration
        .WriteTo
        .Console();
    //info can be output template

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
    // .Seq("http://localhost:5341"); //todo kibana
});

var app = builder.Build();
await app.UseInfrastructure(builder.Configuration);


app.MapGet("api", (IOptions<AppOptions> options) => Results.Ok(options.Value.Name));

app.Run();
