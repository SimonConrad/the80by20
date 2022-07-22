using Core.App.SolutionToProblem;
using Core.Dal.SolutionToProblem;
using Core.Domain.SolutionToProblem;

namespace WebApi.Core;

public class CoreDependencies
{
    public static void AddTo(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ISolutionToProblemAggregateRepository, EfSolutionToProblemAggregateRepository>();
        builder.Services.AddTransient<CreateProblemCommandHandler>();
        builder.Services.AddTransient<ProblemReader>();
    }
}