using Core.App.Administration.MasterData;
using Core.Infrastructure.DAL;

namespace Core.Infrastructure.Administration.MasterData;

public class CategoryCrudRepository : GenericRepository<Category>, ICategoryCrudRepository
{
    public CategoryCrudRepository(CoreDbContext dbCtxt) : base(dbCtxt)
    {
    }
}