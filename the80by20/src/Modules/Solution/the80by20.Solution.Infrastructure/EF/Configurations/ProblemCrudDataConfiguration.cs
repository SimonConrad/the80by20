using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the80by20.Shared.Infrastucture.EF;
using the80by20.Solution.Domain.Operations.Problem;

namespace the80by20.Solution.Infrastructure.EF.Configurations
{
    public class ProblemCrudDataConfiguration : IEntityTypeConfiguration<ProblemCrudData>
    {
        public void Configure(EntityTypeBuilder<ProblemCrudData> builder)
        {
            builder.MapTechnicalProperties();

            builder.HasKey(d => d.AggregateId);

        }
    }
}