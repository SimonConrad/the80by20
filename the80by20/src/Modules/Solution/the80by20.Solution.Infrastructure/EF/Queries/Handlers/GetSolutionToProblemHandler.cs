using Microsoft.EntityFrameworkCore;
using the80by20.Modules.Solution.App.ReadModel;
using the80by20.Modules.Solution.App.Solution.DTO;
using the80by20.Modules.Solution.App.Solution.Queries;
using the80by20.Shared.Abstractions.Queries;

namespace the80by20.Modules.Solution.Infrastructure.EF.Queries.Handlers
{
    internal class GetSolutionToProblemHandler : IQueryHandler<GetSolutionToProblem, SolutionToProblemDto>
    {
        private readonly DbSet<SolutionToProblemReadModel> _solutionToProblemReadModels;

        public GetSolutionToProblemHandler(SolutionDbContext context)
        {
            _solutionToProblemReadModels = context.SolutionsToProblemsReadModel;
        }

        public async Task<SolutionToProblemDto> HandleAsync(GetSolutionToProblem query)
        {
            await _solutionToProblemReadModels
                .SingleOrDefaultAsync(x => x.Id == query.SolutionToProblemId);
            //TODO .AsDto()

            return new SolutionToProblemDto();
        }
    }
}
