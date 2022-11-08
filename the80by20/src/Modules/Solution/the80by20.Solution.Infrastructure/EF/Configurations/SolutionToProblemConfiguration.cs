using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the80by20.Modules.Solution.Domain.Problem;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Infrastucture.EF;

namespace the80by20.Modules.Solution.Infrastructure.EF.Configurations
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