using the80by20.App.Administration.MasterData;
using the80by20.Infrastructure.DAL;
using the80by20.Infrastructure.DAL.DbContext;
using the80by20.Infrastructure.DAL.Misc;

namespace the80by20.Infrastructure.Administration.MasterData;

public class CategoryCrudRepository : GenericRepository<Category>, ICategoryCrudRepository
{
    public CategoryCrudRepository(CoreDbContext dbCtxt) : base(dbCtxt)
    {
    }
}