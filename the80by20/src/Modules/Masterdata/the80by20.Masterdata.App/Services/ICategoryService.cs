using the80by20.Modules.Masterdata.App.DTO;
using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

namespace the80by20.Modules.Masterdata.App.Services
{

    // INFO
    // this is adapter part of port-adapter architecture pattern, this is also application service
    // application / domain layer defines interfaces (ports)
    // which are implemented by adapters in infrastructure layer
    // thanks to this design we achieve that application / domain layer is not dependent upon infra layer (application logic is
    // not depend upon infrastructural implementation details (like connection to database))
    // - it is essence of Inversion of Control principle (keep in mind that data-flow is inverse to this component dependencies)
    // all is setup in module's IoC extensions file and bootstrapped in bootstraper module

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
