using System.Linq.Expressions;
using Common;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DAL;

// INFO generic repository https://codewithmukesh.com/blog/repository-pattern-in-aspnet-core/
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly CoreDbContext Context;

    public GenericRepository(CoreDbContext context)
    {
        Context = context;
    }

    public async Task Add(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
    }

    public async Task AddRange(IEnumerable<T> entities)
    {
       await Context.Set<T>().AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
        Context.Set<T>().Update(entity);
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
    {
        return await Context.Set<T>().Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public void Remove(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    public void  RemoveRange(IEnumerable<T> entities)
    {
        Context.Set<T>().RemoveRange(entities);
    }
}