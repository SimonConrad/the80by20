using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Bootstrapper;

namespace the80by20.Tests.Integration.InMemorySqlLite.Setup
{
    internal sealed class The80By20TestApp : WebApplicationFactory<Program>
    {
        public HttpClient Client { get; }

        public The80By20TestApp(Action<IServiceCollection> services = null)
        {
            Client = WithWebHostBuilder(builder =>
            {
                if (services is Action<IServiceCollection>)
                {
                    builder.ConfigureServices(services);
                }

                builder.UseEnvironment("automatictests");
            }).CreateClient();
        }
    }
}