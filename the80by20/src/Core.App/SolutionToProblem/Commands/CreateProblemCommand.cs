using Core.Domain.SharedKernel;

namespace Core.App.SolutionToProblem.Commands
{
    public class CreateProblemCommand
    {
        public string Description { get; set; }
        
        // TODO Create VO Category
        public string Category { get; set; }

        public Guid UserId { get; set; }

        public SolutionElementType[] SolutionElementTypes { get; set; }
    }
}
