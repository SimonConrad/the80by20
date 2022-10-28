using the80by20.Bootstrapper;
using the80by20.Masterdata.Api;
using the80by20.Shared.Infrastucture;
using the80by20.Solution.Api;
using the80by20.Users.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddBootstrapper()
    .AddSolution(builder.Configuration)
    .AddMasterdata(builder.Configuration)
    .AddUsers(builder.Configuration);

builder.AddLogging();

var app = builder.Build();
app.UseInfrastructure(builder.Configuration);

await app.RunAsync();

public partial class Program { }