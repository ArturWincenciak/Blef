using System.Net.Http.Json;
using Blef.Modules.Games.Application.Queries;

namespace Blef.Modules.Games.Api.Tests.Core;



internal static class HttpApiExtensions
{
    async internal static Task<Guid> MakeNewGame(this HttpClient client)
    {
        var response = await client.PostAsync(GamesUri, content: null);
        response.EnsureSuccessStatusCode();
        var game = await response.Content.ReadFromJsonAsync<Game>();
        return game!.GameId;
    }

    async internal static Task<Guid> JoinPlayer(this HttpClient client, Guid gameId, string nick)
    {
        var response = await client.PostAsJsonAsync(
            requestUri: $"{PlayersUri(gameId)}",
            value: new {Nick = nick});
        response.EnsureSuccessStatusCode();
        var player = await response.Content.ReadFromJsonAsync<Player>();
        return player!.PlayerId;
    }

    async internal static Task<GetPlayerCards.Result> GetCards(this HttpClient client, Guid gameId, Guid playerId)
    {
        var response = await client.GetAsync(requestUri: $"{PlayerUri(gameId, playerId)}/cards");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GetPlayerCards.Result>())!;
    }

    async internal static Task Bid(this HttpClient client, Guid gameId, Guid playerId, string bid)
    {
        var response = await client.PostAsJsonAsync(
            requestUri: $"{PlayerUri(gameId, playerId)}/bids",
            value: new {PokerHand = bid});
        response.EnsureSuccessStatusCode();
    }

    async internal static Task GetGameFlow(this HttpClient client, Guid gameId)
    {
        var response = await client.GetAsync($"{GamesUri}/{gameId}");
        response.EnsureSuccessStatusCode();
    }

    async internal static Task Check(this HttpClient client, Guid gameId, Guid playerId)
    {
        var response = await client.PostAsync(requestUri: $"{PlayerUri(gameId, playerId)}/checks", content: null);
        response.EnsureSuccessStatusCode();
    }

    private static string GamesUri =>
        "games-module/games";

    private static string PlayersUri(Guid gameId) =>
        $"{GamesUri}/{gameId}/players";

    private static string PlayerUri(Guid gameId, Guid playerId) =>
        $"{GamesUri}/{gameId}/players/{playerId}";

    private record Game(Guid GameId);
    private record Player(Guid PlayerId);
}