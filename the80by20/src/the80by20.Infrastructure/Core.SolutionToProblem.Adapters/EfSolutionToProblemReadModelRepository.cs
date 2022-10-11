using Microsoft.EntityFrameworkCore;
using the80by20.App.Core.SolutionToProblem.ReadModel;
using the80by20.App.MasterData.CategoryCrud;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.Core.SolutionToProblem.Operations.Solution;
using the80by20.Domain.SharedKernel.Capabilities;
using the80by20.Infrastructure.DAL.DbContext;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Infrastructure.Core.SolutionToProblem.Adapters
{
    // TODO pass cancelationtoken
    [Adapter]
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

        public IEnumerable<SolutionType> GetSolutionElementTypes()
        {
            IEnumerable<SolutionType> res =  Enum.GetValues(typeof(SolutionType)).Cast<SolutionType>();
            return res;
        }

        public async Task<SolutionToProblemReadModel> GetBySolutionId(SolutionToProblemId id)
        {
            var readModel  = await _coreDbContext.SolutionsToProblemsReadModel
                .FirstOrDefaultAsync(r => r.SolutionToProblemId == id.Value);
            return readModel;
        }

        public async Task<SolutionToProblemReadModel> GetByProblemId(ProblemId id)
        {
            var readModel  = await _coreDbContext.SolutionsToProblemsReadModel
                .FirstOrDefaultAsync(r => r.Id == id.Value);
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

        public async Task<SolutionToProblemReadModel[]> GetByUserId(Guid userId)
        {
            var readModel = await _coreDbContext.SolutionsToProblemsReadModel.Where(rm => rm.UserId == userId).ToArrayAsync();
            return readModel;
        }
    }
}
