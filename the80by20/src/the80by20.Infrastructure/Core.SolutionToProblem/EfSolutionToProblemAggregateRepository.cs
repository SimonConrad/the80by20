using Microsoft.EntityFrameworkCore;
using the80by20.Domain.Core.SolutionToProblem.Operations;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Infrastructure.DAL;
using the80by20.Infrastructure.DAL.DbContext;

namespace the80by20.Infrastructure.Core.SolutionToProblem;

public class EfSolutionToProblemAggregateRepository : ISolutionToProblemAggregateRepository
{
    private readonly CoreDbContext _context;

    public EfSolutionToProblemAggregateRepository(CoreDbContext context)
    {
        _context = context;
    }

    public async Task Create(SolutionToProblemAggregate aggregate)
    {
        _context.SolutionsToProblemsAggregates.Add(aggregate);
        await _context.SaveChangesAsync();
    }

    public async Task<SolutionToProblemAggregate> Get(SolutionToProblemId id)
    {
        var res = await _context.SolutionsToProblemsAggregates.FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }

    public async Task SaveAggragate(SolutionToProblemAggregate aggregate)
    {
        _context.SolutionsToProblemsAggregates.Update(aggregate);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsTheSolutionAssignedToProblem(ProblemId problemId)
    {
        return await _context.SolutionsToProblemsAggregates.AnyAsync(a => a.ProblemId == problemId);
    }
}