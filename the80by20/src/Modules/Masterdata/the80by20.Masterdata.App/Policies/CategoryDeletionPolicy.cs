using the80by20.Masterdata.App.Entities;

namespace the80by20.Masterdata.App.Policies
{
    public class CategoryDeletionPolicy : ICategoryDeletionPolicy
    {
        public Task<bool> CanDeleteAsync(Category category)
        {
            // TODO add policy logic
            return Task.FromResult(true);
        }
    }
}
