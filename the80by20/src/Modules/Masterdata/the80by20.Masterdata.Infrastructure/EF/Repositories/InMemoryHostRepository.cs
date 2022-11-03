using the80by20.Modules.Masterdata.App.Entities;
using the80by20.Modules.Masterdata.App.Repositories;

namespace the80by20.Modules.Masterdata.Infrastructure.EF.Repositories
{
    internal class InMemoryCategoryRepository : ICategoryRepository
    {
        // INFO Not thread-safe, use Concurrent collections
        private readonly List<Category> _categories = new();

        public Task<Category> GetAsync(Guid id) => Task.FromResult(_categories.SingleOrDefault(x => x.Id == id));

        public async Task<IReadOnlyList<Category>> GetAllAsync()
        {
            await Task.CompletedTask;
            return _categories;
        }

        public Task AddAsync(Category category)
        {
            _categories.Add(category);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Category category)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Category category)
        {
            _categories.Remove(category);
            return Task.CompletedTask;
        }
    }
}
