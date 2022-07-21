using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.TacticalDDD;

namespace Core.Domain.SolutionToProblem
{
    // zapis i odczyt w ddd zawsze przez korzeń agregata
    [AggregateRepository]
    public interface ISolutionToProblemAggregateRepository // todo implemnetacja w nowej warstwie Core.Dal
    {
        Task CreateProblem(SolutionToProblemAggregate aggregate, SolutionToProblemData solutionToProblemData);

        // mozliwości persystencji
        // (1) przemapowanie czytem sqlem insert, update
        // (2) kung-fu mapowaniami EF Core
        // (3) dto ze snapshotem (to dto)


        // SolutionToProblemData ma pk, ze fk na SolutionToProblemAggregate, zapisywany w jednej trnskacji tylko przy tworzeniu agregatu
    }
}
