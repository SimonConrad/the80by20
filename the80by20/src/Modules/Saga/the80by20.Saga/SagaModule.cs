using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.Modules;

namespace the80by20.Saga;

public class SagaModule : IModule
{
    public const string BasePath = "saga-module";
    public string Name { get; } = "Saga";
    public string Path => BasePath;

    public void Register(IServiceCollection services)
    {
        //TODO test:
        services.AddSaga();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}