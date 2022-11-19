using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using the80by20.Modules.Solution.Domain.Shared.DomainServices;
using the80by20.Modules.Solution.Domain.Solution.DomainServices;

[assembly: InternalsVisibleTo("the80by20.Tests.Unit")]
namespace the80by20.Modules.Solution.Domain
{
    public static class Extensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<ProblemRejectionDomainService>();
            services.AddSingleton<StartWorkingOnSolutionToProblemDomainService>();
            services.AddSingleton<SetBasePriceForSolutionDomainService>();

            return services;
        }
    }
}