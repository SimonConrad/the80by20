using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using the80by20.Shared.Infrastucture.Time;
using the80by20.Tests.Integration.Setup;
using the80by20.Users.App.Ports;
using the80by20.Users.Infrastructure.Auth;
using Xunit;

namespace the80by20.Tests.Integration.Controllers;

[Collection("api")]
public abstract class ControllerTests : IClassFixture<OptionsProvider>
{
    private readonly IAuthenticator _authenticator;
    protected HttpClient Client { get; }

    protected JwtDto Authorize(Guid userId, string role)
    {
        var jwt = _authenticator.CreateToken(userId, role, "username");
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

        return jwt;
    }

    public ControllerTests(OptionsProvider optionsProvider)
    {
        var authOptions = optionsProvider.Get<AuthOptions>("auth");
        _authenticator = new Authenticator(new OptionsWrapper<AuthOptions>(authOptions), new Clock());
        var app = new The80By20TestApp(ConfigureServices);
        Client = app.Client;
    }
    
    protected virtual void ConfigureServices(IServiceCollection services)
    {
    }
}