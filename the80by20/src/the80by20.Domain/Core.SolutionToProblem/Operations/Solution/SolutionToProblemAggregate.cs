using the80by20.Domain.ArchitectureBuildingBlocks;
using the80by20.Domain.Core.SolutionToProblem.Capabilities;
using the80by20.Domain.Core.SolutionToProblem.Operations.Problem;
using the80by20.Domain.SharedKernel.Capabilities;

namespace the80by20.Domain.Core.SolutionToProblem.Operations.Solution
{

    // TODO przemysleć refactor, w którym mamy dwa agregaty: problem i solution
    // to serwis domenowy decyduje, czy:
    //  - można utworzyć rozwiązanie poprzez sprawdzenie stanu agregatu problemu - rozpcznij prace nad rozwiązaniem
    //  - odrzucenie problemu sktkuje odrzuceniem rozwiązania jeśli takie isnitje
    //  - aby w/w było mozliwe aggregat rozw. musi mieć id problemu 

    // dodatkowo można by trzymać id problemu w agregacie rozwiązania
    // podział na dwa agregaty sprawie, że będą mniejsze
    // serwis domenowy to czytas funkcja dostaje dwa eagregaty i zwraca je ze zmienionym stanem lub rzuć excpetion
    // readmodel nadal solution-to-problem agregaujący zdenormalizowane na cele readmodeli
    [AggregateDdd]
    public class SolutionToProblemAggregate : Versionable, IEquatable<SolutionToProblemAggregate>
    {
        protected SolutionToProblemAggregate() // INFO for EF purpose
        {
        }

        public SolutionToProblemId Id { get; private set; }
        public ProblemId ProblemId { get; private set; }

        public RequiredSolutionTypes RequiredSolutionTypes { get; private set; } = RequiredSolutionTypes.Empty();
       
        public bool WorkingOnSolutionStarted { get; private set; }
        public bool WorkingOnSolutionEnded { get; private set; }

        public Money Price { get; private set; } = Money.Zero();
        public SolutionAbstract SolutionAbstract { get; private set; } = SolutionAbstract.Empty();
        public SolutionElements SolutionElements { get; private set; } = SolutionElements.Empty();

        public static SolutionToProblemAggregate New(ProblemId problemId,
            RequiredSolutionTypes requiredSolutionTypes) 
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

        // INFO only for testing purpose
        // todo
        private void MockStateDataToTestIfEfConverionsWork()
        {
            //SolutionAbstract = SolutionAbstract.FromContent("raz, dwa, trzy");
            //Price = Money.FromValue(123.45m);

            //WorkingOnSolutionStarted = true;
            //WorkingOnSolutionEnded = true;

            //SolutionElements = SolutionElements.Empty()
            //    .Add(SolutionElement.From(SolutionElementType.TheoryOfConceptWithExample, "gdrive-link1"))
            //    .Add(SolutionElement.From(SolutionElementType.PocInCode, "gdrive-link2"));
        }


        public void StartWorkingOnProblemSolution()
        {
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

            if (!SolutionElements.HaveAllRequiredElementTypes(RequiredSolutionTypes))
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
