using Core.App.Administration;

namespace Core.Infrastructure.DAL.Administration;

public class UnitOfWorkOfAdministrationCrud : IUnitOfWorkOfAdministrationCrud
{
    private readonly CoreDbContext _context;

    public UnitOfWorkOfAdministrationCrud(CoreDbContext context,
        ICategoryRepository categoryRepository) // info can add more generic repostiroies interfaces which should be handled in unit of work way, then unit of work makes sense
    {
        _context = context;
        CategoryRepository = categoryRepository;
    }

    public ICategoryRepository CategoryRepository { get; }

    public async Task<int> Commit()
    {
       return await _context.SaveChangesAsync();
    }

    //public void Dispose()
    //{
    //    _context.Dispose();
    //}
}