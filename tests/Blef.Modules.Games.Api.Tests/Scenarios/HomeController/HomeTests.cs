using Blef.Modules.Games.Api.Tests.Core;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HomeController;

public class HomeTests
{
    [Fact]
    public async Task GetHomeTest()
    {
        var result = await new TestBuilder()
            .Home()
            .Build();

        await Verify(result)
            .ScrubMember("RequestTime");
    }
}