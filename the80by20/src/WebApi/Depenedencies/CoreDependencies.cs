using Core.App.SolutionToProblem;
using Core.App.SolutionToProblem.Commands;
using Core.App.SolutionToProblem.Reads;
using Core.Dal.SolutionToProblem;
using Core.Domain.SolutionToProblem;
using Core.Domain.SolutionToProblem.Operations;

namespace WebApi.Depenedencies;

public class CoreDependencies
{
    public static void AddTo(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<CreateProblemCommandHandler>();
        builder.Services.AddTransient<ISolutionToProblemAggregateRepository, EfSolutionToProblemAggregateRepository>();
        builder.Services.AddTransient<ISolutionToProblemReader, EfSolutionToProblemReader>();
    }
}