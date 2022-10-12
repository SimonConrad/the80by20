using Microsoft.Extensions.DependencyInjection;
using the80by20.Solution.Domain.Operations.DomainServices;

namespace the80by20.Solution.Domain;

public static class Extensions
{
    public static IServiceCollection AddSolutionDomain(this IServiceCollection services)
    {
        services.AddSingleton<ProblemRejectionDomainService>();
        services.AddSingleton<StartWorkingOnSolutionToProblemDomainService>();
        services.AddSingleton<SetBasePriceForSolutionToProblemDomainService>();

        return services;
    }
}