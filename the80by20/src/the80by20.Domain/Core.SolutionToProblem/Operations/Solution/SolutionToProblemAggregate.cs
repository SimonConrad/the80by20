using the80by20.Common.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Domain.Core.SolutionToProblem.Capabilities;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.SharedKernel;
using the80by20.Domain.SharedKernel.Capabilities;

namespace the80by20.Domain.Core.SolutionToProblem.Operations.Solution
{
    [AggregateDdd]
    public class SolutionToProblemAggregate : Versionable, IEquatable<SolutionToProblemAggregate>
    {
        protected SolutionToProblemAggregate() // INFO for EF purpose
        {
        }

        public SolutionToProblemId Id { get; private set; }
        // INFO alternative will have one id for problem and solutiontoproblem as problem entity BECOMES solutionToProblem entit
        public ProblemId ProblemId { get; private set; } 
        public RequiredSolutionTypes RequiredSolutionTypes { get; private set; } = RequiredSolutionTypes.Empty();

        public SolutionSummary SolutionSummary { get; private set; } = SolutionSummary.Empty();
        public SolutionElements SolutionElements { get; private set; } = SolutionElements.Empty();
        public Money AddtionalPrice { get; private set; } = Money.Zero();
        public Money BasePrice { get; private set; } = Money.Zero();
        public Money Price => BasePrice + AddtionalPrice;
        public bool WorkingOnSolutionEnded { get; private set; }

        public static SolutionToProblemAggregate New(ProblemId problemId, RequiredSolutionTypes requiredSolutionTypes) 
            => new(SolutionToProblemId.New(), problemId, requiredSolutionTypes);

        private SolutionToProblemAggregate(SolutionToProblemId id,
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
