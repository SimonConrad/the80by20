using the80by20.Masterdata.App.Entities;

namespace the80by20.Masterdata.App.Policies
{
    public interface ICategoryDeletionPolicy
    {
        Task<bool> CanDeleteAsync(Category category);
    }
}
