using the80by20.Masterdata.App.CategoryCrud;
using the80by20.Masterdata.App.CategoryCrud.Ports;

namespace the80by20.Masterdata.Infrastructure.EF.Repositories;

// this is adapter part of port-adapter architecture patter, app / domain layer defines interfaces (ports)
// which adapter part implements, it adapats to the infrastruture
// thanks to this design we achieve taht app layer is not denpdant upon infra layer (application logic is
// not dependat upon implementaion detailas)
// ioc in bootstraper (done in webapi)
public class CategoryCrudRepository : GenericRepository<Category>, ICategoryCrudRepository
{
    public CategoryCrudRepository(MasterDataDbContext dbCtxt) : base(dbCtxt)
    {
    }
}