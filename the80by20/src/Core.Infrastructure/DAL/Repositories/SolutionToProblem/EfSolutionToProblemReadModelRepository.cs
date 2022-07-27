using System.Data.Common;
using Core.App.SolutionToProblem.ReadModel;
using Core.Domain.SolutionToProblem.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.DAL.Repositories.SolutionToProblem
{
    public class EfSolutionToProblemReadModelRepository : ISolutionToProblemReadModelRepository
    {
        private readonly CoreSqlServerDbContext _coreSqlServerDbContext;

        //public EfSolutionToProblemReadModelRepository(CoreSqlServerDbContext coreSqlServerDbContext)
        //{
        //    _coreSqlServerDbContext = coreSqlServerDbContext;
        //}

        private readonly DbContextOptionsBuilder<CoreSqlServerDbContext> optionsBuilder;
        public EfSolutionToProblemReadModelRepository(IOptions<SqlServerOptions> sqlServerOptions)
        {
            //var sqlServerOptions = new SqlServerOptions();
            //configuration.GetSection(Extensions.OptionsSectionSqlServerName);


            optionsBuilder = new DbContextOptionsBuilder<CoreSqlServerDbContext>();
            optionsBuilder.UseSqlServer(sqlServerOptions.Value.ConnectionString);

            _coreSqlServerDbContext = new CoreSqlServerDbContext(optionsBuilder.Options);
        }

        public async Task<SolutionToProblemReadModel> Get(SolutionToProblemId id)
        {
            var readModel  = await _coreSqlServerDbContext.SolutionToProblemReadModel.FirstOrDefaultAsync(r => r.SolutionToProblemId == id.Value);
            return readModel;
        }

        public async Task Create(SolutionToProblemReadModel readModel)
        {
            _coreSqlServerDbContext.SolutionToProblemReadModel.Add(readModel);
            await _coreSqlServerDbContext.SaveChangesAsync();
        }

        public async Task Update(SolutionToProblemReadModel readModel)
        {
            _coreSqlServerDbContext.SolutionToProblemReadModel.Update(readModel);
            await _coreSqlServerDbContext.SaveChangesAsync();
        }

        public async Task<SolutionToProblemAggregate> GetAggregate(SolutionToProblemId id)
        {
            var res = await _coreSqlServerDbContext.SolutionToProblemAggregate.FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }

        public async Task<SolutionToProblemCrudData> GetAggregateCrudData(SolutionToProblemId id)
        {
            var res = await _coreSqlServerDbContext.SolutionToProblemCrudData.FirstOrDefaultAsync(x => x.AggregateId == id.Value);
            return res;
        }
    }
}
