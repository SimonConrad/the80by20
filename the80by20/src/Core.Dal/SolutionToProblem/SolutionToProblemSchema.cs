using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Core.Domain.SolutionToProblem;
using Microsoft.EntityFrameworkCore;

namespace Core.Dal.SolutionToProblem
{
    public static class SolutionToProblemSchema
    {
        public static void MapUsing(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SolutionToProblemAggregate>(e =>
            {
                e.MapBaseEntityProperties();

                e.HasKey(a => a.Id);
                e.Property(a => a.Id)
                    .HasConversion(
                        v => v.Id,
                        v => SolutionToProblemId.FromGuid(v));
                
                e.Ignore(a => a.RequiredSolutionElementTypes);
                e.Ignore(a => a.Confirmed);
                e.Ignore(a => a.Rejected);
                e.Ignore(a => a.WorkingOnSolutionStarted);
                e.Ignore(a => a.Price);
                e.Ignore(a => a.SolutionAbstract);
                e.Ignore(a => a.SolutionElements);

            });
        }
    }
}
