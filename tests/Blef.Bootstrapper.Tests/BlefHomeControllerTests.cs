using Argon;

namespace Blef.Bootstrapper.Tests;

[UsesVerify]
public class BlefHomeControllerTests
{
    [Fact]
    public async Task Home()
    {
        // arrange
        var httpClient = new BlefApplicationFactory(Environments.DEVELOPMENT)
            .CreateClient();

        // act
        var result = await httpClient.GetAsync(string.Empty);

        // assert
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        await Verify(JsonConvert.DeserializeObject(json));
    }

    [Fact]
    public async Task Modules()
    {
        // arrange
        var httpClient = new BlefApplicationFactory(Environments.DEVELOPMENT)
            .CreateClient();

        // act
        var result = await httpClient.GetAsync("/modules");

        // assert
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        await Verify(JsonConvert.DeserializeObject(json));
    }

    [Fact]
    public async Task Swagger()
    {
        // arrange
        var httpClient = new BlefApplicationFactory(Environments.DEVELOPMENT)
            .CreateClient();

        // act
        var result = await httpClient.GetAsync("/swagger/v1/swagger.json");

        // assert
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        await Verify(JsonConvert.DeserializeObject(json));
    }
}