using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace the80by20.Tests.Integration
{
    internal sealed class The80By20TestApp : WebApplicationFactory<Program>
    {
        public HttpClient Client { get; }

        public The80By20TestApp(Action<IServiceCollection> services = null)
        {
            Client = WithWebHostBuilder(builder =>
            {
                if (services is not null)
                {
                    builder.ConfigureServices(services);
                }
            
                builder.UseEnvironment("test");
            }).CreateClient();
        }
    }
}