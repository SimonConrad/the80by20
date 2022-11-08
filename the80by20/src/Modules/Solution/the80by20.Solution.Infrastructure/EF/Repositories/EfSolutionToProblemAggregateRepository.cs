using Microsoft.EntityFrameworkCore;
using the80by20.Modules.Solution.Domain.Problem;
using the80by20.Modules.Solution.Domain.Solution;

namespace the80by20.Modules.Solution.Infrastructure.EF.Repositories
{
    public class EfSolutionToProblemAggregateRepository : ISolutionToProblemAggregateRepository
    {
        private readonly SolutionDbContext _context;

        public EfSolutionToProblemAggregateRepository(SolutionDbContext context)
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
}