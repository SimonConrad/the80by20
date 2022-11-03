using the80by20.Modules.Masterdata.App.DTO;
using the80by20.Modules.Masterdata.App.Entities;
using the80by20.Modules.Masterdata.App.Exceptions;
using the80by20.Modules.Masterdata.App.Policies;
using the80by20.Modules.Masterdata.App.Repositories;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Masterdata.App.Services
{
    [Adapter]
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ICategoryDeletionPolicy categoryPolicy;

        public CategoryService(ICategoryRepository categoryRepository, ICategoryDeletionPolicy categoryPolicy)
        {
            this.categoryRepository = categoryRepository;
            this.categoryPolicy = categoryPolicy;
        }

        public async Task AddAsync(CategoryDto dto)
        {
            dto.Id = Guid.NewGuid();
            await categoryRepository.AddAsync(Category.WithCustomId(dto.Id, dto.Name));
        }

        public async Task<CategoryDetailsDto> GetAsync(Guid id)
        {
            var category = await categoryRepository.GetAsync(id);
            if (category is null)
            {
                throw new ConferenceNotFoundException(id);
            }

            var dto = Map<CategoryDetailsDto>(category);

            dto.Description = category.Description;

            return dto;
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
        {
            var categories = await categoryRepository.GetAllAsync();

            return categories.Select(Map<CategoryDto>).ToList();
        }

        public async Task UpdateAsync(CategoryDetailsDto dto)
        {
            var category = await categoryRepository.GetAsync(dto.Id);
            if (category is null)
            {
                throw new ConferenceNotFoundException(dto.Id);
            }

            category.Update(dto.Name, dto.Description); // TODO think if can change name of the category, maybe not or only based on policy

            await categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await categoryRepository.GetAsync(id);
            if (category is null)
            {
                throw new ConferenceNotFoundException(id);
            }

            if (await categoryPolicy.CanDeleteAsync(category) is false)
            {
                throw new CannotDeleteCategoryException(id);
            }

            await categoryRepository.DeleteAsync(category);
        }

        private static T Map<T>(Category category) where T : CategoryDto, new()
            => new()
            {
                Id = category.Id,
                Name = category.Name,
            };
    }
}
