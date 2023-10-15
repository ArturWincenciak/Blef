using System.Net;
using System.Net.Http.Json;
using Argon;
using Blef.Bootstrapper.Tests.Core;
using Blef.Modules.Games.Application.Commands;

namespace Blef.Bootstrapper.Tests.Tests;

[UsesVerify]
public class InternalServerErrorTests
{
    [Fact]
    public async Task InternalServerErrorOnCommand()
    {
        // arrange
        var httpClient = new BlefApplicationFactory()
            .WithThrowGamesRepository()
            .CreateClient();

        // act
        var requestUri = "games-module/games";
        var result = await httpClient.PostAsync(requestUri, content: null);

        // assert
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        var json = await result.Content.ReadAsStringAsync();
        await Verify(JsonConvert.DeserializeObject(json))
            .ScrubMembers("traceId", "activityId");
    }

    [Fact]
    public async Task InternalServerErrorOnEvent()
    {
        // arrange
        var httpClient = new BlefApplicationFactory()
            .WithThrowGameplayRepository()
            .CreateClient();

        var newGame = await CreateNewGame(httpClient);

        // act
        var requestUri = $"games-module/games/{newGame}/players";
        var requestBody = new { Nick = "Conway" };
        var result = await httpClient.PostAsJsonAsync(requestUri, requestBody);

        // assert
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        var json = await result.Content.ReadAsStringAsync();
        await Verify(JsonConvert.DeserializeObject(json))
            .ScrubMembers("traceId", "activityId");
    }

    private async static Task<Guid> CreateNewGame(HttpClient httpClient)
    {
        var requestUri = "games-module/games";
        var result = await httpClient.PostAsync(requestUri, content: null);
        result.EnsureSuccessStatusCode();
        var json = await result.Content.ReadAsStringAsync();
        var newGame = JsonConvert.DeserializeObject<NewGame.Result>(json);
        return newGame.GameId;
    }
}