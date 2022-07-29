
using Microsoft.Extensions.Options;
using Serilog;
using the80by20.App;
using the80by20.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    //.AddCore() // todo policy for example
    .AddApplication()
    .AddInfrastructure(builder.Configuration);


builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo
        .Console();
    // .WriteTo
    // .File("logs.txt")
    // .WriteTo
    // .Seq("http://localhost:5341");
});

var app = builder.Build();
await app.UseInfrastructure(builder.Configuration);


app.MapGet("api", (IOptions<AppOptions> options) => Results.Ok(options.Value.Name));

app.Run();
