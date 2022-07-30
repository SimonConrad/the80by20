using MediatR;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.SharedKernel.Capabilities;

namespace the80by20.App.Core.SolutionToProblem.Commands
{
    public class UpdatProblemCommand  : IRequest<ProblemId>
    {
        public Guid ProblemId { get; set; }

        public string Description { get; set; }
    
        public Guid Category { get; set; }

        public SolutionType[] SolutionTypes { get; set; }

        public UpdateDataScope UpdateScope { get; set; }

    }

    public enum UpdateDataScope
    {
        All,
        OnlyData,
        OnlySolutionTypes
    }
}
