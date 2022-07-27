using System.Data.Common;
using Core.App.SolutionToProblem.ReadModel;
using Core.Domain.SolutionToProblem.Operations;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DAL.Repositories.SolutionToProblem
{
    public class EfSolutionToProblemReadModelRepository : ISolutionToProblemReadModelRepository
    {
        private readonly CoreSqLiteDbContext _coreSqLiteDbContext;

        //public EfSolutionToProblemReadModelRepository(CoreSqLiteDbContext coreSqLiteDbContext)
        //{
        //    _coreSqLiteDbContext = coreSqLiteDbContext;
        //}

        //private readonly DbContextOptionsBuilder<CoreSqLiteDbContext> optionsBuilder;     
        public EfSolutionToProblemReadModelRepository(DbConnection connection)
        {
            //optionsBuilder = new DbContextOptionsBuilder<CoreSqLiteDbContext>();
            //optionsBuilder.UseSqlite(connection);

            _coreSqLiteDbContext = new CoreSqLiteDbContext(connection);
        }

        public async Task<SolutionToProblemReadModel> Get(SolutionToProblemId id)
        {
            var readModel  = await _coreSqLiteDbContext.SolutionToProblemReadModel.FirstOrDefaultAsync(r => r.SolutionToProblemId == id.Value);
            return readModel;
        }

        public async Task Create(SolutionToProblemReadModel readModel)
        {
            _coreSqLiteDbContext.SolutionToProblemReadModel.Add(readModel);
            await _coreSqLiteDbContext.SaveChangesAsync();
        }

        public async Task Update(SolutionToProblemReadModel readModel)
        {
            _coreSqLiteDbContext.SolutionToProblemReadModel.Update(readModel);
            await _coreSqLiteDbContext.SaveChangesAsync();
        }

        public async Task<SolutionToProblemAggregate> GetAggregate(SolutionToProblemId id)
        {
            var res = await _coreSqLiteDbContext.SolutionToProblemAggregate.FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }

        public async Task<SolutionToProblemCrudData> GetAggregateCrudData(SolutionToProblemId id)
        {
            var res = await _coreSqLiteDbContext.SolutionToProblemCrudData.FirstOrDefaultAsync(x => x.AggregateId == id.Value);
            return res;
        }
    }
}
