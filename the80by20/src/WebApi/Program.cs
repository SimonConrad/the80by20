using Core.App;
using Core.Infrastructure;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    //.AddCore() // todo policy for example
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();
await app.UseInfrastructure(builder.Configuration);


app.MapGet("api", (IOptions<AppOptions> options) => Results.Ok(options.Value.Name));

app.Run();
