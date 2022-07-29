using Microsoft.EntityFrameworkCore;
using the80by20.App.Core.SolutionToProblem.ReadModel;
using the80by20.Domain.Core.SolutionToProblem.Capabilities;
using the80by20.Domain.Core.SolutionToProblem.Operations;
using the80by20.Domain.SharedKernel.Capabilities;
using the80by20.Infrastructure.DAL;
using the80by20.Infrastructure.DAL.Misc;

namespace the80by20.Infrastructure.Core.SolutionToProblem
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

                e.Property(a => a.SolutionAbstract)
                    .HasConversion(
                        a => a.Content,
                        a => SolutionAbstract.FromContent(a));

                e.Property(a => a.Price)
                    .HasConversion(
                        a => a.Value,
                        a => Money.FromValue(a));

                e.Property(a => a.RequiredSolutionElementTypes)
                    .HasConversion(
                        a => a.ToSnapshotInJson(),
                        a => RequiredSolutionElementTypes.FromSnapshotInJson(a));

                e.Property(a => a.SolutionElements)
                    .HasConversion(
                        a => a.ToSnapshotInJson(),
                        a => SolutionElements.FromSnapshotInJson(a));
            });

            modelBuilder.Entity<SolutionToProblemCrudData>(e =>
            {
                e.MapTechnicalProperties();

                e.HasKey(d => d.AggregateId);

            });

            modelBuilder.Entity<SolutionToProblemReadModel>(e =>
            {
                e.HasKey(r => r.SolutionToProblemId);
            });
        }
    }
}
