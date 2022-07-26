using Core.App.SolutionToProblem.Reads;
using Core.Domain.SolutionToProblem.Operations;
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

        public async Task<SolutionToProblemReadModel> Get(SolutionToProblemId id)
        {
            
            var aggregate = await _coreSqLiteDbContext.SolutionToProblemAggregates
                .SingleAsync(s => s.Id == id);

            var data = await _coreSqLiteDbContext.SolutionToProblemDatas
                .SingleAsync(d => d.AggregateId == id.Value);

            // todo in future can think about storing all readmodel data in one rreadmodel specialize for reads as readmodel during Event Storming
            // such readmodel can store duplicated data,
            // and be consistent eventually by mechanism of listerner updating this readmodel subsribing to event (raised from aggragte update)  
            return new()
            {
                SolutionToProblemId = data.AggregateId,
                UserId = data.UserId,
                RequiredSolutionElementTypes = aggregate.RequiredSolutionElementTypes.Elements.Select(t => t.ToString()).ToArray(),
                Description = data.Description,

                IsConfirmed = aggregate.Confirmed,
                IsRejected = aggregate.Rejected,
                WorkingOnSolutionStarted = aggregate.WorkingOnSolutionStarted,
                WorkingOnSolutionEnded = aggregate.WorkingOnSolutionEnded,
                
                Price = aggregate.Price,
                SolutionAbstract = aggregate.SolutionAbstract,
                SolutionElementTypes = aggregate.SolutionElements.Elements.Select(t => t.ToString()).ToArray()
            };
        }
    }
}
