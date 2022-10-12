using System.Linq.Expressions;

namespace the80by20.Shared.Abstractions.Dal;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetById(Guid id);
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
    Task Add(T entity);
    Task Update(T entity);
    Task AddRange(IEnumerable<T> entities);
    Task Remove(T entity);
    Task RemoveRange(IEnumerable<T> entities);

}