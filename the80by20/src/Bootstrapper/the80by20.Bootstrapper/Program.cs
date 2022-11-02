using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using System.Reflection;
using the80by20.Bootstrapper;
using the80by20.Shared.Abstractions.Modules;
using the80by20.Shared.Infrastucture;
using the80by20.Shared.Infrastucture.Modules;
using the80by20.Solution.Api;
using the80by20.Users.Api;

public partial class Program
{
    private static IList<Assembly> assemblies;
    private static IList<IModule> modules;

    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Log.Logger = CreateLogger(builder);
        
        builder.Host
            .UseSerilog()
            .ConfigureModules();

        try
        {
            Log.Information("Starting host");

            var configuration = builder.Configuration;

            IList<Assembly> assemblies = ModuleLoader.LoadAssemblies(configuration);
            IList<IModule> modules = ModuleLoader.LoadModules(assemblies);

            // INFO https://github.com/serilog/serilog-extensions-hosting 
            Log.Logger.Information($"Modules: {string.Join(", ", modules.Select(x => x.Name))}");

            AddServices(builder, modules);

            WebApplication app = builder.Build();
            UseMiddlewares(app, builder.Configuration, modules);

            assemblies.Clear();
            modules.Clear();

            await app.RunAsync();
        }
        catch (Exception ex)
        {
            // INFO workaround for exception thrown by ef migrations https://github.com/dotnet/runtime/issues/60600
            string type = ex.GetType().Name;
            if (type.Equals("StopTheHostException", StringComparison.Ordinal))
            {
                throw;
            }

            Log.Logger.Fatal(ex, "Unhandled exception");
            await Task.CompletedTask;
        }
        finally
        {
            Log.Logger.Information("Application ended");
            Log.CloseAndFlush();
        }
    }

    private static Serilog.Core.Logger CreateLogger(WebApplicationBuilder builder)
    {
       return new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                //"Microsoft.EntityFrameworkCore.Database.Command": "Warning",
                //"Microsoft.EntityFrameworkCore.Infrastructure": "Warning"
                .WriteTo
                .Console()
                .Enrich.FromLogContext()
                .WriteTo.File(builder.Configuration.GetValue<string>("LogFilePath"),
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        retainedFileCountLimit: 10,
                        fileSizeLimitBytes: 4194304, // 4MB
                                                     //restrictedToMinimumLevel: LogEventLevel.Information,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] ({SourceContext}.{Method}) {Message}{NewLine}{Exception}")
                .CreateLogger();
        // todo .{Method} is not logging method name
        //loggerConfiguration.ReadFrom.Configuration(builder.Configuration);

        // .WriteTo
        // .File("logs.txt")
        // .WriteTo
        // .Seq("http://localhost:5341"); //todo kibana and appinsights
    }

    private static void AddServices(WebApplicationBuilder builder, IList<IModule> modules)
    {
        builder.Services
            .AddInfrastructure(builder.Configuration, assemblies, modules);

        foreach (var module in modules)
        {
            module.Register(builder.Services);
        }
    }

    private static WebApplication UseMiddlewares(WebApplication app, IConfiguration configuration, IList<IModule> modules)
    {
        app.UseInfrastructure(configuration);

        foreach (var module in modules)
        {
            module.Use(app);
        }

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("api", (IOptions<AppOptions> options) => Results.Ok(options.Value.Name));
        });
        return app;
    }
}


public partial class Program { }