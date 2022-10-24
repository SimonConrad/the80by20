using the80by20.Masterdata.App.Entities;

namespace the80by20.Masterdata.App.Repositories;

// INFO Altenrative way - use IGenericRepository<Category>
// public interface ICategoryRepository : IGenericRepository<Category>
public interface ICategoryRepository
{
    Task<Category> GetAsync(Guid id);

    Task<IReadOnlyList<Category>> GetAllAsync();

    Task AddAsync(Category host);

    Task UpdateAsync(Category host);

    Task DeleteAsync(Category host);
}