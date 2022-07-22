using Core.Dal;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Core
{
    public class ProblemReader
    {
        private readonly CoreSqLiteDbContext _coreSqLiteDbContext;

        public ProblemReader(CoreSqLiteDbContext coreSqLiteDbContext)
        {
            _coreSqLiteDbContext = coreSqLiteDbContext;
        }

        public async Task<CreateProblemDto> Get()
        {
            var res = await _coreSqLiteDbContext.SolutionToProblemAggregates.FirstOrDefaultAsync();

            return new CreateProblemDto();
        }
    }
}
