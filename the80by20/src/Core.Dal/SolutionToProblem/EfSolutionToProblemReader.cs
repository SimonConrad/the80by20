using Core.App.SolutionToProblem.Reads;
using Microsoft.EntityFrameworkCore;

namespace Core.Dal.SolutionToProblem
{
    public class EfSolutionToProblemReader : ISolutionToProblemReader
    {
        private readonly CoreSqLiteDbContext _coreSqLiteDbContext;

        public EfSolutionToProblemReader(CoreSqLiteDbContext coreSqLiteDbContext)
        {
            _coreSqLiteDbContext = coreSqLiteDbContext;
        }

        public async Task<SolutionToProblemReadModel> Get(Guid solutionToProblemId)
        {
            var res = await _coreSqLiteDbContext.SolutionToProblemAggregates
                .SingleAsync(s => s.Id == solutionToProblemId);

            return new()
            {
                SolutionToProblemId = res.Id
            };
        }
    }
}
