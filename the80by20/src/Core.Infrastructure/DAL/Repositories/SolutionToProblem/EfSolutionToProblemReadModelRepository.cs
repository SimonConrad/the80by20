using Core.App.SolutionToProblem.ReadModel;
using Core.Domain.SolutionToProblem.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.DAL.Repositories.SolutionToProblem
{
    public class EfSolutionToProblemReadModelRepository : ISolutionToProblemReadModelRepository
    {
        private readonly CoreDbContext _coreDbContext;

        public EfSolutionToProblemReadModelRepository(IOptions<DatabaseOptions> dbOptions)
        {
            _coreDbContext = new CoreDbContext(CoreDbContextFactory.Create(dbOptions.Value).Options);
        }

        //public EfSolutionToProblemReadModelRepository(CoreDbContext coreDbContext)
        //{
        //    _coreDbContext = coreDbContext;
        //}

        public async Task<SolutionToProblemReadModel> Get(SolutionToProblemId id)
        {
            var readModel  = await _coreDbContext.SolutionToProblemReadModel
                .FirstOrDefaultAsync(r => r.SolutionToProblemId == id.Value);
            return readModel;
        }

        public async Task Create(SolutionToProblemReadModel readModel)
        {
            _coreDbContext.SolutionToProblemReadModel.Add(readModel);
            await _coreDbContext.SaveChangesAsync();
        }

        public async Task Update(SolutionToProblemReadModel readModel)
        {
            _coreDbContext.SolutionToProblemReadModel.Update(readModel);
            await _coreDbContext.SaveChangesAsync();
        }

        public async Task<SolutionToProblemAggregate> GetAggregate(SolutionToProblemId id)
        {
            var res = await _coreDbContext.SolutionToProblemAggregate.FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }

        public async Task<SolutionToProblemCrudData> GetAggregateCrudData(SolutionToProblemId id)
        {
            var res = await _coreDbContext.SolutionToProblemCrudData.FirstOrDefaultAsync(x => x.AggregateId == id.Value);
            return res;
        }
    }
}
