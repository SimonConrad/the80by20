using Chronicle;
using Microsoft.Extensions.DependencyInjection;

namespace the80by20.Saga;

public static class Extensions
{
    public static IServiceCollection AddSaga(this IServiceCollection services)
    {
        // TODO debug saga
        services.AddChronicle();
        return services;
    }
}