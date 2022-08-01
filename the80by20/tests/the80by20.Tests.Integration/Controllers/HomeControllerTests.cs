using Shouldly;
using Xunit;

namespace the80by20.Tests.Integration.Controllers;

public class HomeControllerTests : ControllerTests
{
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
}