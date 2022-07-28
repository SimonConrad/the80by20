using Common;
using Core.App.Administration;

namespace Core.Infrastructure.DAL.Administration;

public class CategoryCrudRepository : GenericRepository<Category>, ICategoryCrudRepository
{
    public CategoryCrudRepository(CoreDbContext dbCtxt) : base(dbCtxt)
    {
    }
}