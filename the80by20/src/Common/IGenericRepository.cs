using System.Linq.Expressions;

namespace Common;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
    Task Add(T entity);
    void Update(T entity);
    Task AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}