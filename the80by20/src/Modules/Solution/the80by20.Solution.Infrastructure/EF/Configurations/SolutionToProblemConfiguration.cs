using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the80by20.Shared.Infrastucture.EF;
using the80by20.Solution.Domain.Operations;
using the80by20.Solution.Domain.Operations.Problem;

namespace the80by20.Solution.Infrastructure.EF.Configurations
{
    public class ProblemAggregateConfiguration : IEntityTypeConfiguration<ProblemAggregate>
    {
        public void Configure(EntityTypeBuilder<ProblemAggregate> builder)
        {
            builder.MapTechnicalProperties();

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasConversion(
                    a => a.Value,
                    a => new ProblemId(a));

            builder.Property(a => a.RequiredSolutionTypes)
                .HasConversion(
                    a => a.ToSnapshotInJson(),
                    a => RequiredSolutionTypes.FromSnapshotInJson(a));
        }
    }
}