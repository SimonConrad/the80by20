using the80by20.Modules.Masterdata.App.Entities;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Masterdata.App.Policies
{
    // INFO 
    // Value of modeling (using Event Storming) before coding is that at modeling phase policies are discovered 
    [PolicyDdd]
    public class CategoryDeletionPolicy : ICategoryDeletionPolicy
    {
        public Task<bool> CanDeleteAsync(Category category)
        {
            // TODO add policy logic
            return Task.FromResult(true);
        }
    }
}
