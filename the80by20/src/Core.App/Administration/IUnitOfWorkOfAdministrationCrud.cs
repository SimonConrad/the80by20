namespace Core.App.Administration;

public interface IUnitOfWorkOfAdministrationCrud
{
    // INFO make sense if there are more crud like repositories done with generic repository pattern that mus be handled in one transaction
    ICategoryRepository CategoryRepository { get; }
    Task<int> Commit();
}