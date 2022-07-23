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
            var aggregate = await _coreSqLiteDbContext.SolutionToProblemAggregates
                .SingleAsync(s => s.Id == solutionToProblemId);

            var data = await _coreSqLiteDbContext.SolutionToProblemDatas
                .SingleAsync(d => d.AggregateId == solutionToProblemId);

            return new()
            {
                SolutionToProblemId = aggregate.Id,
                UserId = data.UserId,
                Description = data.Description
            };
        }
    }
}
