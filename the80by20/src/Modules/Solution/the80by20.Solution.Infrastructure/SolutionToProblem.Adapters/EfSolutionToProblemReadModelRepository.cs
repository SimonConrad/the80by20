using Microsoft.EntityFrameworkCore;
using the80by20.Masterdata.App.CategoryCrud;
using the80by20.Masterdata.App.CategoryCrud.Ports;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel.Capabilities;
using the80by20.Solution.App.SolutionToProblem.ReadModel;
using the80by20.Solution.Domain.SolutionToProblem.Operations.Problem;
using the80by20.Solution.Domain.SolutionToProblem.Operations.Solution;
using the80by20.Solution.Infrastructure.DAL.DbContext;

namespace the80by20.Solution.Infrastructure.SolutionToProblem.Adapters
{
    // TODO pass cancelationtoken
    [Adapter]
    public class EfSolutionToProblemReadModelRepository : ISolutionToProblemReadModelQueries, ISolutionToProblemReadModelUpdates
    {
        private readonly CoreDbContext _coreDbContext;
        private readonly ICategoryCrudRepository categoryCrudRepository;

        // todo pobrać kategorie przez interfejs w warstwie plaikacyjnej modulu masterdata
        public EfSolutionToProblemReadModelRepository(CoreDbContext coreDbContext,
            ICategoryCrudRepository categoryCrudRepository)
        {
            _coreDbContext = coreDbContext;
            this.categoryCrudRepository = categoryCrudRepository;
        }

        public async Task<Category[]> GetProblemsCategories()
        {
            IEnumerable<Category> res = await categoryCrudRepository.GetAll();

            return res.ToArray();
        }

        public IEnumerable<SolutionType> GetSolutionElementTypes()
        {
            IEnumerable<SolutionType> res = Enum.GetValues(typeof(SolutionType)).Cast<SolutionType>();
            return res;
        }

        public async Task<SolutionToProblemReadModel> GetBySolutionId(SolutionToProblemId id)
        {
            var readModel = await _coreDbContext.SolutionsToProblemsReadModel
                .FirstOrDefaultAsync(r => r.SolutionToProblemId == id.Value);
            return readModel;
        }

        public async Task<SolutionToProblemReadModel> GetByProblemId(ProblemId id)
        {
            var readModel = await _coreDbContext.SolutionsToProblemsReadModel
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
