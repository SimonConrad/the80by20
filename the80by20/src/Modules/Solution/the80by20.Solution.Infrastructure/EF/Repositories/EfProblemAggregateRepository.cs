using Microsoft.EntityFrameworkCore;
using the80by20.Solution.Domain.Operations.Problem;

namespace the80by20.Solution.Infrastructure.EF.Repositories
{
    public class EfProblemAggregateRepository : IProblemAggregateRepository
    {
        private readonly SolutionDbContext _context;

        public EfProblemAggregateRepository(SolutionDbContext context)
        {
            _context = context;
        }

        public async Task Create(ProblemAggregate aggregate, ProblemCrudData crudData)
        {
            _context.ProblemsAggregates.Add(aggregate);
            _context.ProblemsCrudData.Add(crudData);
            await _context.SaveChangesAsync();
        }

        public async Task<ProblemAggregate> Get(ProblemId id)
        {
            return await _context.ProblemsAggregates.SingleAsync(a => a.Id == id);
        }

        public async Task<ProblemCrudData> GetCrudData(ProblemId id)
        {
            return await _context.ProblemsCrudData.SingleAsync(a => a.AggregateId == id.Value);
        }

        public async Task SaveAggragate(ProblemAggregate aggregate)
        {
            _context.ProblemsAggregates.Update(aggregate);
            await _context.SaveChangesAsync();

        }

        public async Task SaveData(ProblemCrudData crudData)
        {
            _context.ProblemsCrudData.Update(crudData);
            await _context.SaveChangesAsync();
        }
    }
}