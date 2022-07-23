using Core.App.SolutionToProblem.Reads;
using Core.Dal;
using Microsoft.EntityFrameworkCore;

namespace WebApi.SolutionToProblemReads
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
                .SingleAsync(s => s.Id.Value == solutionToProblemId); // TODO equtability of ID value object

            return new()
            {
                SolutionToProblemId = res.Id.Value
            };
        }
    }
}
