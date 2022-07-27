using Core.Domain.SolutionToProblem.Operations;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DAL.Repositories.SolutionToProblem;

public class EfSolutionToProblemAggregateRepository : ISolutionToProblemAggregateRepository
{
    private readonly CoreDbContext _context;

    public EfSolutionToProblemAggregateRepository(CoreDbContext context)
    {
        _context = context;
    }

    public async Task Create(SolutionToProblemAggregate aggregate, SolutionToProblemCrudData crudData)
    {
        _context.SolutionToProblemAggregate.Add(aggregate);
        _context.SolutionToProblemCrudData.Add(crudData);
        await _context.SaveChangesAsync();
    }

    public async Task<SolutionToProblemAggregate> Get(SolutionToProblemId id)
    {
        var res = await _context.SolutionToProblemAggregate.FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }

    public async Task<SolutionToProblemCrudData> GetCrudData(SolutionToProblemId id)
    {
        var res = await _context.SolutionToProblemCrudData.FirstOrDefaultAsync(x => x.AggregateId == id.Value);
        return res;
    }

    public async Task SaveAggragate(SolutionToProblemAggregate aggregate)
    {
        _context.SolutionToProblemAggregate.Update(aggregate);
        await _context.SaveChangesAsync();
    }

    public async Task SaveData(SolutionToProblemCrudData crudData)
    {
        _context.SolutionToProblemCrudData.Update(crudData);
        await _context.SaveChangesAsync();
    }
}