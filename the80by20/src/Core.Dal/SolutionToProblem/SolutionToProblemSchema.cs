using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Core.Domain.SolutionToProblem;
using Core.Domain.SolutionToProblem.Capabilities;
using Core.Domain.SolutionToProblem.Operations;
using Microsoft.EntityFrameworkCore;

namespace Core.Dal.SolutionToProblem
{
    public static class SolutionToProblemSchema
    {
        public static void MapUsing(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SolutionToProblemAggregate>(e =>
            {
                e.MapTechnicalProperties();

                e.HasKey(a => a.Id);
                e.Property(a => a.Id)
                    .HasConversion(
                        a => a.Value,
                        a => new SolutionToProblemId(a));

                e.Ignore(a => a.RequiredSolutionElementTypes);
                e.Ignore(a => a.Confirmed);
                e.Ignore(a => a.Rejected);
                e.Ignore(a => a.WorkingOnSolutionStarted);
                e.Ignore(a => a.Price);
                e.Ignore(a => a.SolutionAbstract);
                e.Ignore(a => a.SolutionElements);
            });


            // TODO Configure ef table-object mapping that SolutionToProblemData.AggregateId is PK, that is also FK referencing SolutionToProblemAggregate.Id
            // but without navigation proporties, to not have possibility of lazy loading (problem: aggregate boundaries can be unitionally to wide - when loaded dependcies with lazy loading, - to broad transaction boundary)
            //modelBuilder.Entity<SolutionToProblemData>(e =>
            //{
            //    e.MapBaseEntityProperties();


            //    e.HasOne<SolutionToProblemAggregate>()
            //        .WithOne().HasForeignKey()

            //    // e.HasKey(d => d.AggregateId);

            //    //e.HasOne<SolutionToProblemAggregate>

            //});


            modelBuilder.Entity<SolutionToProblemData>(e =>
            {
                e.MapTechnicalProperties();

                e.HasKey(a => a.AggregateId);

            });
        }
    }
}
