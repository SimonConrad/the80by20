using Common;
using Common.TacticalDDD;
using Core.Domain.SharedKernel;

namespace Core.Domain.SolutionToProblem
{
    [AggregateDdd]
    public class SolutionToProblemAggregate : BaseEntity
    {
        protected SolutionToProblemAggregate() // INFO for EF purpose
        {
        }

        public SolutionToProblemId Id { get; private set; } 
        public RequiredSolutionElementTypes RequiredSolutionElementTypes { get; init; } 

        public static SolutionToProblemAggregate New(RequiredSolutionElementTypes requiredSolutionElementTypes) => new(SolutionToProblemId.New(), requiredSolutionElementTypes);

        private SolutionToProblemAggregate(SolutionToProblemId id, RequiredSolutionElementTypes requiredSolutionElementTypes)
        {
            Id = id;
            RequiredSolutionElementTypes = requiredSolutionElementTypes;
        }

        public bool Confirmed { get; private set; }
        public bool Rejected { get; private set; }
        public bool WorkingOnSolutionStarted { get; private set; }
        public bool WorkingOnSolutionEnded { get; private set; }

        public Price Price { get; private set; } = new();
        public SolutionAbstract SolutionAbstract { get; private set; }  = new();
        public SolutionElements SolutionElements { get; private set; } = new();

        public void ConfirmProblem()
        {
            Confirmed = true;
        }

        public void RejectProblem()
        {
            Rejected = true;
        }

        public void StartWorkingOnProblemSolution()
        {
            if (!Confirmed)
            {
                throw new SolutionToProblemException("Cannot start working on not confirmed problem");
            }

            WorkingOnSolutionStarted = true;

        }

        public void SetAbstract(SolutionAbstract solutionAbstract)
        {
            SolutionAbstract = solutionAbstract;
        }

        public void SetPrice(Price price)
        {
            Price = price;

        }

        public void AddSolutionElement(SolutionElement solutionElement)
        {
            SolutionElements.Add(solutionElement);
        }

        public void RemoveSolutionElement(SolutionElement solutionElement)
        {
            SolutionElements.Remove(solutionElement);
        }

        public void EndWorkingOnProblemSolution()
        {
            if (!WorkingOnSolutionStarted)
            {
                throw new SolutionToProblemException("Cannot end not started solution");
            }

            if (!Price.HasValue())
            {
                throw new SolutionToProblemException("Cannot end solution without price");
            }

            if (!SolutionAbstract.NotEmpty())
            {
                throw new SolutionToProblemException("Cannot end solution without abstract");
            }

            if (!SolutionElements.HaveAllRequiredElementTypes(RequiredSolutionElementTypes))
            {
                throw new SolutionToProblemException("Cannot end solution without required elment types");
            }

            WorkingOnSolutionEnded = true;
        }
    }
}
