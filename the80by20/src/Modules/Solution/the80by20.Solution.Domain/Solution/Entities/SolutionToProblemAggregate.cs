using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Modules.Solution.Domain.Solution.Events;
using the80by20.Modules.Solution.Domain.Solution.Exceptions;
using the80by20.Modules.Solution.Domain.Solution.ValueObjects;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;
using the80by20.Shared.Abstractions.Kernel.Capabilities;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.Domain.Solution.Entities
{


    [AggregateDdd]
    public sealed class SolutionToProblemAggregate : AggregateRoot
    {

        // INFO
        // alternative way - have one id for ProblemAggregate and SolutionToProblemAggregate
        // as problem entity BECOMES solutionToProblem entity

        public ProblemId ProblemId { get; private set; }
        public RequiredSolutionTypes RequiredSolutionTypes { get; private set; } = RequiredSolutionTypes.Empty();
        public SolutionSummary SolutionSummary { get; private set; } = SolutionSummary.Empty();
        public SolutionElements SolutionElements { get; private set; } = SolutionElements.Empty();
        public Money AddtionalPrice { get; private set; } = Money.Zero();
        public Money BasePrice { get; private set; } = Money.Zero();
        public Money Price => BasePrice + AddtionalPrice;
        public bool WorkingOnSolutionEnded { get; private set; }

        // INFO
        // for EF purpose
        protected SolutionToProblemAggregate()
        {
        }

        private SolutionToProblemAggregate(AggregateId id,
           ProblemId problemId,
           RequiredSolutionTypes requiredSolutionTypes,
           int version = 0)
        {
            Id = id;
            ProblemId = problemId;
            RequiredSolutionTypes = requiredSolutionTypes;
            Version = version;


            //MockStateDataToTestIfEfConverionsWork();
        }

        //private SolutionToProblemAggregate(AggregateId id, ProblemId problemId) => (Id, ProblemId) = (id, problemId);

        // INFO
        // creation method for creating new aggregates - place for validations,
        // when object exists and is retrieved from db, EF is mapping values to porerties and this method is not used
        public static SolutionToProblemAggregate New(ProblemId problemId, RequiredSolutionTypes requiredSolutionTypes)
        {
            var solution = new SolutionToProblemAggregate(Guid.NewGuid(), problemId, requiredSolutionTypes);
            solution.ClearEvents();
            solution.Version = 0;
            return solution;
        }


        // info
        // internal so that domain layer can access this method, called by SetBasePriceForSolutionToProblemDomainService
        internal void SetBasePrice(Money price)
        {
            BasePrice = price;
            IncrementVersion();
        }

        public void SetSummary(SolutionSummary solutionSummary)
        {
            SolutionSummary = solutionSummary;
            IncrementVersion();
        }

        public void SetAdditionalPrice(Money price)
        {
            AddtionalPrice = price;
            IncrementVersion();
        }

        public void AddSolutionElement(SolutionElement solutionElement)
        {
            SolutionElements = SolutionElements.Add(solutionElement);
            IncrementVersion();
        }

        public void RemoveSolutionElement(SolutionElement solutionElement)
        {
            SolutionElements = SolutionElements.Remove(solutionElement);
            IncrementVersion();
        }

        public void FinishWorkOnSolutionToProblem()
        {
            if (!BasePrice.HasValue())
            {
                // TODO think about dedidicates exception or pass namoef() so that it cab be testes properly
                throw new SolutionToProblemException("Cannot end solution to problem without price", Id.Value);
            }

            if (SolutionSummary.IsEmpty())
            {
                throw new SolutionToProblemException("Cannot end solution to problem without abstract", Id.Value);
            }

            if (!SolutionElements.HaveAllRequiredElementTypes(RequiredSolutionTypes))
            {
                throw new SolutionToProblemException("Cannot end solution to problem without required elment types", Id.Value);
            }

            WorkingOnSolutionEnded = true;

            AddEvent(new SolutionFinished(this));
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
    }
}
