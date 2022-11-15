using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Modules.Users.Infrastructure;
using the80by20.Shared.Abstractions.Modules;

namespace the80by20.Modules.Users.Api
{
    internal class UsersModule : IModule
    {
        public const string BasePath = "users";

        public string Name => "Users";

        public string Path => BasePath;

        public IEnumerable<string> Policies { get; } = new[]
        {
            "users"
        };

        public void Register(IServiceCollection services)
        {
            // INFO if needed service can be obtain by such code:
            // using var scope = services.BuildServiceProvider();
            // var configuration = scope.GetService<IConfiguration>();
            // so we don't need to create a constructor with passed to it dependencies

            services.AddInfrastructure();
        }

        public void Use(IApplicationBuilder app)
        {
            // INFO if needed service can be obtain by such code:
            // app.ApplicationServices.GetService...
            // so we don't need to create a constructor with passed to it dependencies
        }
    }
}
