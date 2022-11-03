using the80by20.Modules.Masterdata.App.Entities;

namespace the80by20.Modules.Masterdata.App.Policies
{
    public interface ICategoryDeletionPolicy
    {
        Task<bool> CanDeleteAsync(Category category);
    }
}
