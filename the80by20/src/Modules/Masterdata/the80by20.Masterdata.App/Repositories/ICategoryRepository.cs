using the80by20.Modules.Masterdata.App.Entities;

namespace the80by20.Modules.Masterdata.App.Repositories;

// INFO
// Alternative way - use ICategoryRepository : IGenericRepository<Category>
// however watch out for generic repository as it can be tricky to do custom code
//
// Generics on the infrastructure level (infrastructure level mechanisms) of the application is OK,
// but often in context of composition or inheritance then as ultimate solution
// however in the code representing business (application / domain layer) making generic and common mechanisms is not good idea

public interface ICategoryRepository
{
    Task<Category> GetAsync(Guid id);

    Task<IReadOnlyList<Category>> GetAllAsync();

    Task AddAsync(Category host);

    Task UpdateAsync(Category host);

    Task DeleteAsync(Category host);
}