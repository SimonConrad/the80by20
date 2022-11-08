using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the80by20.Modules.Solution.Domain.Problem;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Modules.Solution.Domain.Solution;
using the80by20.Shared.Abstractions.Kernel.Capabilities;
using the80by20.Shared.Abstractions.Kernel.Types;
using the80by20.Shared.Infrastucture.EF;

namespace the80by20.Modules.Solution.Infrastructure.EF.Configurations
{
    public class SolutionToProblemAggregateConfiguration : IEntityTypeConfiguration<SolutionToProblemAggregate>
    {
        public void Configure(EntityTypeBuilder<SolutionToProblemAggregate> builder)
        {
            // was before: builder.MapTechnicalProperties();
            builder
               .Property(x => x.Version)
               .IsConcurrencyToken();

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    x => x.Value,
                    x => new AggregateId(x));

            builder.Property(x => x.ProblemId)
                .HasConversion(
                    x => x.Value,
                    x => new ProblemId(x));

            builder.Property(a => a.SolutionSummary)
                .HasConversion(
                    x => x.Content,
                    x => SolutionSummary.FromContent(x));

            builder.Property(a => a.BasePrice)
                .HasConversion(
                    x => x.Value,
                    x => Money.FromValue(x));

            builder.Property(a => a.AddtionalPrice)
                .HasConversion(
                    x => x.Value,
                    x => Money.FromValue(x));

            builder.Property(a => a.RequiredSolutionTypes)
                .HasConversion(
                    x => x.ToSnapshotInJson(),
                    x => RequiredSolutionTypes.FromSnapshotInJson(x));

            builder.Property(a => a.SolutionElements)
                .HasConversion(
                    x => x.ToSnapshotInJson(),
                    x => SolutionElements.FromSnapshotInJson(x));
        }
    }
}