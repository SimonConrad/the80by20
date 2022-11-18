using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using the80by20.Shared.Abstractions.Auth;
using the80by20.Shared.Infrastucture.Auth;
using the80by20.Shared.Infrastucture.Time;
using the80by20.Tests.Integration.InMemorySqlLite.Setup;
using Xunit;

namespace the80by20.Tests.Integration.InMemorySqlLite.Controllers;

[Collection("api")]
public abstract class ControllerTests : IClassFixture<OptionsProvider>
{
    private readonly IAuthManager _authenticator;
    protected HttpClient Client { get; }

    protected JsonWebToken Authorize(Guid userId, string role, IDictionary<string, IEnumerable<string>> claims = null, string email = null)
    {
        var jwt = _authenticator.CreateToken(userId.ToString(), role, "username", claims: claims, email: email);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

        return jwt;
    }

    public ControllerTests(OptionsProvider optionsProvider)
    {
        var authOptions = optionsProvider.Get<AuthOptions>("auth");
        _authenticator = new AuthManager(authOptions, new Clock());
        var app = new The80By20TestApp(ConfigureServices);
        Client = app.Client;
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
    }
}