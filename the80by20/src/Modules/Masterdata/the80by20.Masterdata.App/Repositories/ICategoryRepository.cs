using the80by20.Masterdata.App.Entities;
using the80by20.Shared.Abstractions.Dal;

namespace the80by20.Masterdata.App.Repositories;

// INFO exposed api for other layers
// TODO do below and move info to i-category-service
// INFO recfactor to category-service (with deletion policy) exposed via i-category-service and category-dto
// (incoming port used by solution-module)
public interface ICategoryRepository : IGenericRepository<Category>
{

}