using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the80by20.Modules.Solution.Domain.Problem;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Modules.Solution.Domain.Solution;
using the80by20.Shared.Abstractions.SharedKernel.Capabilities;
using the80by20.Shared.Infrastucture.EF;

namespace the80by20.Modules.Solution.Infrastructure.EF.Configurations
{
    public class SolutionToProblemAggregateConfiguration : IEntityTypeConfiguration<SolutionToProblemAggregate>
    {
        public void Configure(EntityTypeBuilder<SolutionToProblemAggregate> builder)
        {
            builder.MapTechnicalProperties();

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasConversion(
                    a => a.Value,
                    a => new SolutionToProblemId(a));

            builder.Property(a => a.ProblemId)
                .HasConversion(
                    a => a.Value,
                    a => new ProblemId(a));

            builder.Property(a => a.SolutionSummary)
                .HasConversion(
                    a => a.Content,
                    a => SolutionSummary.FromContent(a));

            builder.Property(a => a.BasePrice)
                .HasConversion(
                    a => a.Value,
                    a => Money.FromValue(a));

            builder.Property(a => a.AddtionalPrice)
                .HasConversion(
                    a => a.Value,
                    a => Money.FromValue(a));

            builder.Property(a => a.RequiredSolutionTypes)
                .HasConversion(
                    a => a.ToSnapshotInJson(),
                    a => RequiredSolutionTypes.FromSnapshotInJson(a));

            builder.Property(a => a.SolutionElements)
                .HasConversion(
                    a => a.ToSnapshotInJson(),
                    a => SolutionElements.FromSnapshotInJson(a));
        }
    }
}