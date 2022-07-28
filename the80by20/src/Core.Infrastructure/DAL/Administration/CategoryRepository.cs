using Core.App.Administration;

namespace Core.Infrastructure.DAL.Administration;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(CoreDbContext dbContext) : base(dbContext)
    {
    }
}