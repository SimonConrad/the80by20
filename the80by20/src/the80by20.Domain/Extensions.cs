using Microsoft.Extensions.DependencyInjection;
using the80by20.Domain.Core.SolutionToProblem.Operations;

namespace the80by20.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddSingleton<ProblemRejectionDomainService>();
        services.AddSingleton<StartWorkingOnSolutionToProblemDomainService>();

        return services;
    }
}