using Core.App;
using Core.App.SolutionToProblem.ReadModel;
using Core.Infrastructure;
using Core.Infrastructure.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    //.AddCore() todo  policy for example
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();
await app.UseInfrastructure(builder.Configuration);


app.MapGet("api", (IOptions<AppOptions> options) => Results.Ok(options.Value.Name));

app.Run();
