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

    public async Task CreateProblem(SolutionToProblemAggregate aggregate, SolutionToProblemData data)
    {
        _context.SolutionToProblemAggregates.Add(aggregate);
        _context.SolutionToProblemDatas.Add(data);
        await _context.SaveChangesAsync();
    }
}