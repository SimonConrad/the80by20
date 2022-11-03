using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the80by20.Modules.Solution.Domain.Operations.Problem;
using the80by20.Shared.Infrastucture.EF;

namespace the80by20.Modules.Solution.Infrastructure.EF.Configurations
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