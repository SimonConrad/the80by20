using Microsoft.Extensions.DependencyInjection;
using the80by20.Solution.Domain.Operations.DomainServices;

namespace the80by20.Solution.Domain
{
    internal static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<ProblemRejectionDomainService>();
            services.AddSingleton<StartWorkingOnSolutionToProblemDomainService>();
            services.AddSingleton<SetBasePriceForSolutionToProblemDomainService>();

            return services;
        }
    }
}