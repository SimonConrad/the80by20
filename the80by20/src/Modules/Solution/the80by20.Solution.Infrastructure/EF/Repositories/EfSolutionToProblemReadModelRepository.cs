using Microsoft.EntityFrameworkCore;
using the80by20.Modules.Masterdata.App.DTO;
using the80by20.Modules.Masterdata.App.Services;
using the80by20.Modules.Solution.App.ReadModel;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.Infrastructure.EF.Repositories
{
    // TODO pass cancelationtoken
    [Adapter]
    public class EfSolutionToProblemReadModelRepository : ISolutionToProblemReadModelQueries, ISolutionToProblemReadModelUpdates
    {
        private readonly SolutionDbContext _coreDbContext;
        private readonly ICategoryService _categoryService;

        public EfSolutionToProblemReadModelRepository(SolutionDbContext coreDbContext,
            ICategoryService categoryService)
        {
            _coreDbContext = coreDbContext;
            _categoryService = categoryService;
        }

        public async Task<CategoryDto[]> GetProblemsCategories()
        {
            IEnumerable<CategoryDto> res = await _categoryService.GetAllAsync();

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
