using Common.DDD;
using Core.Domain.SharedKernel.Capabilities;
using Core.Domain.SolutionToProblem.Capabilities;

namespace Core.Domain.SolutionToProblem.Operations
{
    [AggregateDdd]
    public class SolutionToProblemAggregate : BaseEntity, IEquatable<SolutionToProblemAggregate>
    {
        protected SolutionToProblemAggregate() // INFO for EF purpose
        {
        }

        public SolutionToProblemId Id { get; private set; }

        public RequiredSolutionElementTypes RequiredSolutionElementTypes { get; private set; } = RequiredSolutionElementTypes.Empty();
        

        public bool Confirmed { get; private set; }
        public bool Rejected { get; private set; }
        public bool WorkingOnSolutionStarted { get; private set; }
        public bool WorkingOnSolutionEnded { get; private set; }

        public Money Price { get; private set; } = Money.Zero();
        public SolutionAbstract SolutionAbstract { get; private set; } = SolutionAbstract.Empty();

        public SolutionElements SolutionElements { get; private set; } = SolutionElements.Empty();

        public static SolutionToProblemAggregate New(RequiredSolutionElementTypes requiredSolutionElementTypes) 
            => new(SolutionToProblemId.New(), requiredSolutionElementTypes);

        private SolutionToProblemAggregate(Guid id, RequiredSolutionElementTypes requiredSolutionElementTypes)
        {
            Id = id;
            RequiredSolutionElementTypes = requiredSolutionElementTypes;

            MockStateDataToTestIfEfConverionsWork();
        }

        // INFO only for testing purpose
        private void MockStateDataToTestIfEfConverionsWork()
        {
            SolutionAbstract = SolutionAbstract.FromContent("raz, dwa, trzy");
            Price = Money.FromValue(123.45m);

            Confirmed = true;
            Rejected = true;
            WorkingOnSolutionStarted = true;
            WorkingOnSolutionEnded = true;

            SolutionElements = SolutionElements.Empty()
                .Add(SolutionElement.From(SolutionElementType.TheoryOfConceptWithExample, "gdrive-link1"))
                .Add(SolutionElement.From(SolutionElementType.PocInCode, "gdrive-link2"));
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
            SolutionElements = SolutionElements.Add(solutionElement);
        }

        public void RemoveSolutionElement(SolutionElement solutionElement)
        {
            SolutionElements = SolutionElements.Remove(solutionElement);
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

        public bool Equals(SolutionToProblemAggregate? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SolutionToProblemAggregate)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
