using Core.Domain.SolutionToProblem;
using Core.Domain.SolutionToProblem.Operations;

namespace Core.Dal.SolutionToProblem;

public class EfSolutionToProblemAggregateRepository : ISolutionToProblemAggregateRepository
{
    private readonly CoreSqLiteDbContext _context;


    public EfSolutionToProblemAggregateRepository(CoreSqLiteDbContext context)
    {
        _context = context;
    }

    public async Task CreateProblem(SolutionToProblemAggregate aggregate, SolutionToProblemData solutionToProblemData)
    {
        _context.SolutionToProblemAggregates.Add(aggregate);
        await _context.SaveChangesAsync();
    }
}