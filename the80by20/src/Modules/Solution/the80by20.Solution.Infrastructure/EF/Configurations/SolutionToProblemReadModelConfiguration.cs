using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using the80by20.Modules.Solution.App.ReadModel;

namespace the80by20.Modules.Solution.Infrastructure.EF.Configurations
{
    public class SolutionToProblemReadModelConfiguration : IEntityTypeConfiguration<SolutionToProblemReadModel>
    {
        public void Configure(EntityTypeBuilder<SolutionToProblemReadModel> builder)
        {
            builder.HasKey(r => r.Id);
        }
    }
}