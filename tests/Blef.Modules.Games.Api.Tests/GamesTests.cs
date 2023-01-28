using System.Net.Http.Json;

namespace Blef.Modules.Games.Api.Tests;

public record Game(Guid GameId);

public record Player(Guid PlayerId);

public class GamesTests
{
    private const string GAMES = "games-module/games";

    [Fact]
    public async Task PlayGame()
    {
        var client = new BlefApplicationFactory().CreateClient();

        var createGameResponse = await client.PostAsync(GAMES, content: null);
        createGameResponse.EnsureSuccessStatusCode();
        var game = await createGameResponse.Content.ReadFromJsonAsync<Game>();

        var joinPlayerOneResponse = await client.PostAsJsonAsync(
            requestUri: $"{GAMES}/{game!.GameId}/players", value: new {Nick = "Player One"});
        joinPlayerOneResponse.EnsureSuccessStatusCode();
        var playerOne = await joinPlayerOneResponse.Content.ReadFromJsonAsync<Player>();

        var joinPlayerTwoResponse = await client.PostAsJsonAsync(
            requestUri: $"{GAMES}/{game.GameId}/players", value: new {Nick = "Player Two"});
        joinPlayerTwoResponse.EnsureSuccessStatusCode();
        var playerTwo = await joinPlayerTwoResponse.Content.ReadFromJsonAsync<Player>();

        var playerOneCardsResponse = await client.GetAsync(
            $"{GAMES}/{game.GameId}/players/{playerOne!.PlayerId}/cards");
        playerOneCardsResponse.EnsureSuccessStatusCode();

        var playerTwoCardsResponse = await client.GetAsync(
            $"{GAMES}/{game.GameId}/players/{playerTwo!.PlayerId}/cards");
        playerTwoCardsResponse.EnsureSuccessStatusCode();

        var playerOneBidResponse = await client.PostAsJsonAsync(
            $"{GAMES}/{game.GameId}/players/{playerOne.PlayerId}/bids",
            new {PokerHand = "one-of-a-kind:nine"});
        playerOneBidResponse.EnsureSuccessStatusCode();

        var playerTwoBidResponse = await client.PostAsJsonAsync(
            $"{GAMES}/{game.GameId}/players/{playerTwo.PlayerId}/bids",
            new {PokerHand = "one-of-a-kind:ten"});
        playerTwoBidResponse.EnsureSuccessStatusCode();

        var playerOneCheckResponse = await client.PostAsync(
            $"{GAMES}/{game.GameId}/players/{playerOne.PlayerId}/checks", content: null);
        playerOneCheckResponse.EnsureSuccessStatusCode();

        var gameFlowResponse = await client.GetAsync($"{GAMES}/{game.GameId}");
        gameFlowResponse.EnsureSuccessStatusCode();
    }
}