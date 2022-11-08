using Microsoft.Extensions.DependencyInjection;
using the80by20.Modules.Solution.Domain.Shared.DomainServices;

namespace the80by20.Modules.Solution.Domain
{
    public static class Extensions
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