using Microsoft.Extensions.DependencyInjection;

namespace the80by20.Shared.Infrastucture.Services
{
    public interface IDataSeeder
    {
        Task Seed(IServiceScope scope, CancellationToken cancellationToken);
    }
}
