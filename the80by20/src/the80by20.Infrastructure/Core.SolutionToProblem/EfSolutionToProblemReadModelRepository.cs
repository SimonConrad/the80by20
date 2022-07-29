using Microsoft.EntityFrameworkCore;
using the80by20.App.Administration.MasterData;
using the80by20.App.Core.SolutionToProblem.ReadModel;
using the80by20.Domain.Core.SolutionToProblem.Operations;
using the80by20.Domain.SharedKernel.Capabilities;
using the80by20.Infrastructure.DAL;
using the80by20.Infrastructure.DAL.DbContext;

namespace the80by20.Infrastructure.Core.SolutionToProblem
{
    // TODO pass cancelationtoken
    public class EfSolutionToProblemReadModelRepository : ISolutionToProblemReadModelQueries, ISolutionToProblemReadModelUpdates
    {
        private readonly CoreDbContext _coreDbContext;

        public EfSolutionToProblemReadModelRepository(CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        public async Task<Category[]> GetProblemsCategories()
        {
            var res = await _coreDbContext.Categories.ToArrayAsync();

            return res;
        }

        public IEnumerable<SolutionElementType> GetSolutionElementTypes()
        {
            IEnumerable<SolutionElementType> res =  Enum.GetValues(typeof(SolutionElementType)).Cast<SolutionElementType>();
            return res;
        }

        public async Task<SolutionToProblemReadModel> Get(SolutionToProblemId id)
        {
            var readModel  = await _coreDbContext.SolutionsToProblemsReadModel
                .FirstOrDefaultAsync(r => r.SolutionToProblemId == id.Value);
            return readModel;
        }

        public async Task Create(SolutionToProblemReadModel readModel)
        {
            _coreDbContext.SolutionsToProblemsReadModel.Add(readModel);
            await _coreDbContext.SaveChangesAsync();
        }

        public async Task Update(SolutionToProblemReadModel readModel)
        {
            _coreDbContext.SolutionsToProblemsReadModel.Update(readModel);
            await _coreDbContext.SaveChangesAsync();
        }
    }
}
