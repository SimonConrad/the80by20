using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.Modules;
using the80by20.Users.App;
using the80by20.Users.Infrastructure;

namespace the80by20.Users.Api
{
    // TODO apply base controller and other module mechanisms from masterdatamodule
    internal class UsersModule : IModule
    {
        public const string BasePath = "solution";

        public string Name => "Users";

        public string Path => BasePath;

        public void Register(IServiceCollection services)
        {
            // INFO if needed service can be obtain by such code:
            // using var scope = serviceProvider.CreateScope();
            // scope.ServiceProvider.GetService...
            // so we don't need to create a constructor with passed to it dependencies
            using var scope = services.BuildServiceProvider();
            var configuration = scope.GetService<IConfiguration>();

            services.AddApp();
            services.AddInfrastructure(configuration);
        }

        public void Use(IApplicationBuilder app)
        {
            // INFO if needed service can be obtain by such code:
            // app.ApplicationServices.GetService...
            // so we don't need to create a constructor with passed to it dependencies
        }
    }
}
