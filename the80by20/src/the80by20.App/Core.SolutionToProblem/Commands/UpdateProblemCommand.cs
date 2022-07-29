using the80by20.Domain.SharedKernel.Capabilities;

namespace the80by20.App.Core.SolutionToProblem.Commands
{
    public class UpdatProblemCommand
    {
        public string Description { get; set; }
    
        public Guid Category { get; set; }

        public SolutionElementType[] SolutionElementTypes { get; set; }
    }
}
