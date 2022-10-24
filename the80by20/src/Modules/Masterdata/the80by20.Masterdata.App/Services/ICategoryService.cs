using the80by20.Masterdata.App.DTO;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Masterdata.App.Services
{

    // INFO this is adapter part of port-adapter architecture pattern,
    // app / domain layer defines interfaces (ports)
    // which are implemented by adapters in infrastructure layer
    // thanks to this design we achieve that app / domain layer is not dependant upon infra layer (application logic is
    // not dependat upon infrastructural implementaion detailas (like connection to database))
    // all is setuped  in module's ioc  extensions file and bootstrapped in bootstraper module

    // TODO set all types accessors as internal

    [Port]
    public interface ICategoryService
    {
        Task AddAsync(CategoryDto dto);

        Task<CategoryDetailsDto> GetAsync(Guid id);
        
        Task<IReadOnlyList<CategoryDto>> GetAllAsync();
        
        Task UpdateAsync(CategoryDetailsDto dto);
        
        Task DeleteAsync(Guid id);
    }
}
