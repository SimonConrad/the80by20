using Microsoft.EntityFrameworkCore;
using the80by20.Shared.Abstractions.DomainLayer.SharedKernel.Capabilities;
using the80by20.Shared.Infrastucture.EF;
using the80by20.Solution.Domain.SolutionToProblem.Capabilities;
using the80by20.Solution.Domain.SolutionToProblem.Operations;
using the80by20.Solution.Domain.SolutionToProblem.Operations.Problem;
using the80by20.Solution.Domain.SolutionToProblem.Operations.Solution;

namespace the80by20.Solution.Infrastructure.SolutionToProblem.Adapters
{
    public static class SolutionToProblemSchema
    {
        public static void MapUsing(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProblemAggregate>(e =>
            {
                e.MapTechnicalProperties();

                e.HasKey(a => a.Id);
                e.Property(a => a.Id)
                    .HasConversion(
                        a => a.Value,
                        a => new ProblemId(a));

                e.Property(a => a.RequiredSolutionTypes)
                    .HasConversion(
                        a => a.ToSnapshotInJson(),
                        a => RequiredSolutionTypes.FromSnapshotInJson(a));
            });


            modelBuilder.Entity<SolutionToProblemAggregate>(e =>
            {
                e.MapTechnicalProperties();

                e.HasKey(a => a.Id);
                e.Property(a => a.Id)
                    .HasConversion(
                        a => a.Value,
                        a => new SolutionToProblemId(a));

                e.Property(a => a.ProblemId)
                    .HasConversion(
                        a => a.Value,
                        a => new ProblemId(a));

                e.Property(a => a.SolutionSummary)
                    .HasConversion(
                        a => a.Content,
                        a => SolutionSummary.FromContent(a));

                e.Property(a => a.BasePrice)
                    .HasConversion(
                        a => a.Value,
                        a => Money.FromValue(a));

                e.Property(a => a.AddtionalPrice)
                    .HasConversion(
                        a => a.Value,
                        a => Money.FromValue(a));

                e.Property(a => a.RequiredSolutionTypes)
                    .HasConversion(
                        a => a.ToSnapshotInJson(),
                        a => RequiredSolutionTypes.FromSnapshotInJson(a));

                e.Property(a => a.SolutionElements)
                    .HasConversion(
                        a => a.ToSnapshotInJson(),
                        a => SolutionElements.FromSnapshotInJson(a));
            });


            modelBuilder.Entity<ProblemCrudData>(e =>
            {
                e.MapTechnicalProperties();

                e.HasKey(d => d.AggregateId);

            });

            modelBuilder.Entity<SolutionToProblemReadModel>(e =>
            {
                e.HasKey(r => r.Id);
            });
        }
    }
}
