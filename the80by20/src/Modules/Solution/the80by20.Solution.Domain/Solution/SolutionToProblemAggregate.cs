using the80by20.Modules.Solution.Domain.Solution;
using the80by20.Modules.Solution.Domain.Problem;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Capabilities;
using the80by20.Shared.Abstractions.Kernel;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.Domain.Solution
{
    [AggregateDdd]
    public class SolutionToProblemAggregate : AggregateRoot
    {
        protected SolutionToProblemAggregate() // INFO for EF purpose
        {
        }

        // INFO alternative way - have one id for ProblemAggregate and SolutionToProblemAggregate as problem entity BECOMES solutionToProblem entity
        public ProblemId ProblemId { get; private set; }
        public RequiredSolutionTypes RequiredSolutionTypes { get; private set; } = RequiredSolutionTypes.Empty();

        public SolutionSummary SolutionSummary { get; private set; } = SolutionSummary.Empty();
        public SolutionElements SolutionElements { get; private set; } = SolutionElements.Empty();
        public Money AddtionalPrice { get; private set; } = Money.Zero();
        public Money BasePrice { get; private set; } = Money.Zero();
        public Money Price => BasePrice + AddtionalPrice;
        public bool WorkingOnSolutionEnded { get; private set; }

        public static SolutionToProblemAggregate New(ProblemId problemId, RequiredSolutionTypes requiredSolutionTypes)
            => new(new Guid(), problemId, requiredSolutionTypes);

        private SolutionToProblemAggregate(AggregateId id,
            ProblemId problemId,
            RequiredSolutionTypes requiredSolutionTypes)
        {
            Id = id;
            ProblemId = problemId;
            RequiredSolutionTypes = requiredSolutionTypes;


            MockStateDataToTestIfEfConverionsWork();
        }

        // infointernal so that domain layer can access this method, called by SetBasePriceForSolutionToProblemDomainService
        internal void SetBasePrice(Money price)
        {
            BasePrice = price;
        }

        public void SetSummary(SolutionSummary solutionSummary)
        {
            SolutionSummary = solutionSummary;
        }

        public void SetAdditionalPrice(Money price)
        {
            AddtionalPrice = price;
        }

        public void AddSolutionElement(SolutionElement solutionElement)
        {
            SolutionElements = SolutionElements.Add(solutionElement);
        }

        public void RemoveSolutionElement(SolutionElement solutionElement)
        {
            SolutionElements = SolutionElements.Remove(solutionElement);
        }

        public void FinishWorkOnSolutionToProblem()
        {
            if (!BasePrice.HasValue())
            {
                // TODO think about dedidicates exception or pass namoef() so that it cab be testes properly
                throw new SolutionToProblemException("Cannot end solution to problem without price");
            }

            if (SolutionSummary.IsEmpty())
            {
                throw new SolutionToProblemException("Cannot end solution to problem without abstract");
            }

            if (!SolutionElements.HaveAllRequiredElementTypes(RequiredSolutionTypes))
            {
                throw new SolutionToProblemException("Cannot end solution to problem without required elment types");
            }

            WorkingOnSolutionEnded = true;
        }

        // TODO remove in future and write intgrations test for testing mapping purposes
        private void MockStateDataToTestIfEfConverionsWork()
        {
            //SolutionAbstract = SolutionAbstract.FromContent("raz, dwa, trzy");
            //Price = Money.FromValue(123.45m);
            //WorkingOnSolutionEnded = true;

            //SolutionElements = SolutionElements.Empty()
            //    .Add(SolutionElement.From(SolutionElementType.TheoryOfConceptWithExample, "gdrive-link1"))
            //    .Add(SolutionElement.From(SolutionElementType.PocInCode, "gdrive-link2"));
        }

        public bool Equals(SolutionToProblemAggregate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SolutionToProblemAggregate)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
