using Core.App;
using Core.App.SolutionToProblem.ReadModel;
using Core.Infrastructure;
using Core.Infrastructure.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddOptions();

builder.Services
    //.AddCore() todo 
    .AddApplication()
    .AddInfrastructure(builder.Configuration);


var app = builder.Build();
app.UseInfrastructure();

//const string optionsSectionAppName = "app";
//// todo move to infrastruture
//if (builder.Configuration.GetOptions<AppOptions>(optionsSectionAppName).SqlLiteEnabled)
//{
//    using (var serviceScope = app.Services.CreateScope())
//    {
//        var context = serviceScope.ServiceProvider.GetRequiredService<CoreSqLiteDbContext>();
//        await context.Database.EnsureCreatedAsync();
//    }
//}


app.MapGet("api", (IOptions<AppOptions> options) => Results.Ok(options.Value.Name));


app.Run();
