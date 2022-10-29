﻿using Microsoft.EntityFrameworkCore;
using the80by20.Masterdata.App.Entities;
using the80by20.Masterdata.App.Repositories;

namespace the80by20.Masterdata.Infrastructure.EF.Repositories;

public class CategoryRepository: ICategoryRepository
{
    private readonly MasterDataDbContext dbCtxt;
    private readonly DbSet<Category> categories;

    public CategoryRepository(MasterDataDbContext dbCtxt)
    {
        this.dbCtxt = dbCtxt;
        categories = dbCtxt.Categories;
    }

    public async Task<IReadOnlyList<Category>> GetAllAsync() => await categories.ToListAsync();

    public async Task<Category> GetAsync(Guid id) => await categories.SingleOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(Category host)
    {
        await categories.AddAsync(host);
        await dbCtxt.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category host)
    {
        categories.Update(host);
        await dbCtxt.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category host)
    {
        categories.Remove(host);
        await dbCtxt.SaveChangesAsync();
    }
}