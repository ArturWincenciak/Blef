using Argon;

namespace Blef.Modules.Users.Api.Tests;

[UsesVerify]
public class HomeControllerTests
{
    [Fact]
    public async Task Home()
    {
        // arrange
        var httpClient = new BlefApplicationFactory()
            .CreateClient();

        // act
        var result = await httpClient.GetAsync("users-module/home");

        // assert
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        await Verify(JsonConvert.DeserializeObject(json));
    }
}