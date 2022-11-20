using Chronicle;
using Microsoft.Extensions.DependencyInjection;

namespace the80by20.Saga;

public static class Extensions
{
    public static IServiceCollection AddSaga(this IServiceCollection services)
    {
        services.AddChronicle();
        return services;
    }
}