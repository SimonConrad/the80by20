using Core.App.Administration;
using Core.App.SolutionToProblem.ReadModel;
using Core.Domain.SharedKernel.Capabilities;
using Core.Domain.SolutionToProblem.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.DAL.SolutionToProblem
{
    // TODO pass cancelationtoken
    public class EfSolutionToProblemReadModelRepository : ISolutionToProblemReadModelRepository
    {
        private readonly CoreDbContext _coreDbContext;

        public EfSolutionToProblemReadModelRepository(CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        public async Task<Category[]> GetProblemsCategories()
        {
            var res = await _coreDbContext.Category.ToArrayAsync();

            return res;
        }

        public IEnumerable<SolutionElementType> GetSolutionElementTypes()
        {
            IEnumerable<SolutionElementType> res =  Enum.GetValues(typeof(SolutionElementType)).Cast<SolutionElementType>();
            return res;
        }

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
    }
}
