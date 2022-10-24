using the80by20.Masterdata.App.Entities;
using the80by20.Masterdata.App.Repositories;

namespace the80by20.Masterdata.Infrastructure.EF.Repositories;

public class CategoryRepository: ICategoryRepository
{
    public CategoryRepository(MasterDataDbContext dbCtxt)
    {
    }

    public Task AddAsync(Category host)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Category host)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Category>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Category host)
    {
        throw new NotImplementedException();
    }
}