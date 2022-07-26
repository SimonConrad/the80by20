using Core.App.SolutionToProblem.ReadModel;
using Core.Domain.SolutionToProblem.Operations;
using Microsoft.EntityFrameworkCore;

namespace Core.Dal.SolutionToProblem
{
    public class EfSolutionToProblemReadModelRepository : ISolutionToProblemReadModelRepository
    {
        private readonly CoreSqLiteDbContext _coreSqLiteDbContext;

        public EfSolutionToProblemReadModelRepository(CoreSqLiteDbContext coreSqLiteDbContext)
        {
            _coreSqLiteDbContext = coreSqLiteDbContext;
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
    }
}
