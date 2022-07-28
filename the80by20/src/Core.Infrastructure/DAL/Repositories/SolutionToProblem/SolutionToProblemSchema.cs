using Common;
using Core.App.SolutionToProblem.ReadModel;
using Core.Domain.SharedKernel.Capabilities;
using Core.Domain.SolutionToProblem.Capabilities;
using Core.Domain.SolutionToProblem.Operations;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DAL.Repositories.SolutionToProblem
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
