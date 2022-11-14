using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the80by20.Modules.Solution.Domain.Problem.Entities;
using the80by20.Modules.Solution.Domain.Shared;
using the80by20.Shared.Abstractions.Kernel.Types;

namespace the80by20.Modules.Solution.Infrastructure.EF.Configurations
{
    // INFO 
    // Lazy loading should not be enebaled for aggregates
    public class ProblemAggregateConfiguration : IEntityTypeConfiguration<ProblemAggregate>
    {
        public void Configure(EntityTypeBuilder<ProblemAggregate> builder)
        {
            builder.HasKey(x => x.Id);

            // was before: builder.MapTechnicalProperties();
            builder
               .Property(x => x.Version)
               .IsConcurrencyToken();

            builder
                .Property(x => x.Id)
                .HasConversion(a => a.Value, a => new AggregateId(a));

            builder
                .Property(x => x.RequiredSolutionTypes)
                .HasConversion(x => x.ToSnapshotInJson(), x => RequiredSolutionTypes.FromSnapshotInJson(x));
        }
    }
}