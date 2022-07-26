using Common.DDD;
using Core.Domain.SharedKernel.Capabilities;
using Core.Domain.SolutionToProblem.Capabilities;

namespace Core.Domain.SolutionToProblem.Operations
{
    [AggregateDdd]
    public class SolutionToProblemAggregate : BaseEntity
    {
        protected SolutionToProblemAggregate() // INFO for EF purpose
        {
        }

        public SolutionToProblemId Id { get; private set; }

        public RequiredSolutionElementTypes RequiredSolutionElementTypes { get; private set; } 

        public SolutionElements SolutionElements { get; private set; }


        public bool Confirmed { get; private set; }
        public bool Rejected { get; private set; }
        public bool WorkingOnSolutionStarted { get; private set; }
        public bool WorkingOnSolutionEnded { get; private set; }

        public Money Price { get; private set; } = Money.Zero();
        public SolutionAbstract SolutionAbstract { get; private set; } = SolutionAbstract.Empty();

        public static SolutionToProblemAggregate New(RequiredSolutionElementTypes requiredSolutionElementTypes) 
            => new(SolutionToProblemId.New(), requiredSolutionElementTypes);

        private SolutionToProblemAggregate(Guid id, RequiredSolutionElementTypes requiredSolutionElementTypes)
        {
            Id = id;
            RequiredSolutionElementTypes = requiredSolutionElementTypes;

            TestIfEfConverionsWork();
        }

        // TODO only for testing purpose
        private void TestIfEfConverionsWork()
        {
            SolutionAbstract = SolutionAbstract.FromContent("raz, dwa, trzy");
            Price = Money.FromValue(123.45m);

            Confirmed = true;
            Rejected = true;
            WorkingOnSolutionStarted = true;
            WorkingOnSolutionEnded = true;
        }

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

        public void SetPrice(Money price)
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
