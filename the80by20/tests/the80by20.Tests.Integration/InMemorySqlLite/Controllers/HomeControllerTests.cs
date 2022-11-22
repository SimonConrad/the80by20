using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Data;
using the80by20.Tests.Integration.Common;
using the80by20.Tests.Integration.InMemorySqlLite.Setup;
using Xunit;

namespace the80by20.Tests.Integration.InMemorySqlLite.Controllers;


// INFO Test Doubles (by Martin Fowler):
//
// Dummy - objects are passed around but never actually used. Usually they are just used to fill parameter lists.
//
// Fake - objects usually have working implmentations, but usually take same shortcut which makes
// them not suitable for production (an in-memory-test-database is a good example)
//
// Stubs - provide canned answers to calls made during the test,
// usually not responding at all to anything outside what is programmed in for the test.
//
// Spies - are stubs that also record some information based on how they were called.
// One form of this might be an email service that records how many messages it was sent.
//
// Mocks - are pre-programmed with exceptions which form a specificaiton of the calls are expected to receive,
// they can throw an exception if they receive a call they do not expect and are checked during verification to ensure
// they got all the calls they were expecting.


// INFO to run tests sequentially - otherwise database problems
[Collection(nameof(SystemTestCollectionDefinition))]
public class HomeControllerTests : ControllerTests, IDisposable
{

    public SqliteConnection Connection { get; private set; }

    //[Fact]
    public async Task get_base_endpoint_should_return_200_ok_status_code_and_api_name()
    {
        SqlLiteIneMemoryManager.RecreateDbs(Connection);

        var response = await Client.GetAsync("/api");
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldBe("\"The 80 by 20 [test]\"");
    }

    public HomeControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
    }


    // INFO run before each tests - before UsersControllerTests as this method is inoked in ControllerTests
    protected override void ConfigureServices(IServiceCollection services)
    {
        Connection = new SqliteConnection("Filename=:memory:");
        Connection.Open();

        SqlLiteIneMemoryManager.SetupIoCContainer(services, Connection);
    }

    // INFO run after each test
    public void Dispose()
    {
        if (Connection.State == ConnectionState.Open)
        {
            Connection.Close();
            Connection.Dispose();
        }
    }
}