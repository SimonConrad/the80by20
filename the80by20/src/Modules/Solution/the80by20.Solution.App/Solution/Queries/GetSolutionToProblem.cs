using the80by20.Modules.Solution.App.Solution.DTO;
using the80by20.Shared.Abstractions.Queries;

namespace the80by20.Modules.Solution.App.Solution.Queries
{
    public class GetSolutionToProblem : IQuery<SolutionToProblemDto>
    {
        public Guid SolutionToProblemId { get; set; }
    }
}
