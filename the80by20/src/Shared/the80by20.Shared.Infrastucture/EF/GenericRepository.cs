using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using the80by20.Shared.Abstractions.Dal;

namespace the80by20.Shared.Infrastucture.EF;

// INFO generic repository https://codewithmukesh.com/blog/repository-pattern-in-aspnet-core/
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly DbContext Context;

    public GenericRepository(DbContext context)
    {
        Context = context;
    }
    public async Task Add(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task AddRange(IEnumerable<T> entities)
    {
        await Context.Set<T>().AddRangeAsync(entities);
        await Context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
    {
        return await Context.Set<T>().Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public async Task Remove(T entity)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task RemoveRange(IEnumerable<T> entities)
    {
        Context.Set<T>().RemoveRange(entities);
        await Context.SaveChangesAsync();
    }
}