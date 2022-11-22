using the80by20.Services.Sale.App.DTO;

namespace the80by20.Services.Sale.App.Services;

public interface IClientService
{
    Task CreateAsync(ClientDto dto);
}