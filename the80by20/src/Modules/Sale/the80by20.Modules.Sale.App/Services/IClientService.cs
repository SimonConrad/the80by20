using the80by20.Modules.Sale.App.DTO;

namespace the80by20.Modules.Sale.App.Services;

public interface IClientService
{
    Task CreateAsync(ClientDto dto);
}