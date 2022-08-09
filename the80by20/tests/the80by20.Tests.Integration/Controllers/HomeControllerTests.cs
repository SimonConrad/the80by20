using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace the80by20.Tests.Integration.Controllers;

public class HomeControllerTests : ControllerTests
{
    private SqliteConnection _connection;
    private IWithCoreDbContext _testDatabase;

    [Fact]
    public async Task get_base_endpoint_should_return_200_ok_status_code_and_api_name()
    {
        var response = await Client.GetAsync("/api");
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldBe("\"The 80 by 20 [test]\"");
    }

    public HomeControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        ApplySqlLite(services);
    }

    private void ApplySqlLite(IServiceCollection services)
    {
        var components = SqlLiteIneMemoryComponentsSetupper.Setup(services);
        _connection = components.connection;
        _testDatabase = components.ctxt;
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
        _connection.Dispose();
    }
}