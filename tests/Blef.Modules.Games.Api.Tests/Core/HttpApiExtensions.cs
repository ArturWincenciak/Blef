using System.Net;
using System.Net.Http.Json;
using Blef.Modules.Games.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Xunit.Sdk;

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

    async internal static Task BidWithSuccess(this HttpClient client, Guid gameId, Guid playerId, string bid)
    {
        var response = await Bid(client, gameId, playerId, bid);
        response.EnsureSuccessStatusCode();
    }

    async internal static Task<ProblemDetails> BidWithRuleViolation(this HttpClient client, Guid gameId, Guid playerId, string bid)
    {
        var response = await Bid(client, gameId, playerId, bid);
        if (response.StatusCode != HttpStatusCode.BadRequest)
            throw new AssertActualExpectedException(
                expected: HttpStatusCode.BadRequest,
                actual: response.StatusCode,
                userMessage: "Expected that this call to be rejected but it unexpectedly succeeded");

        return (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
    }

    private async static Task<HttpResponseMessage> Bid(HttpClient client, Guid gameId, Guid playerId, string bid) =>
        await client.PostAsJsonAsync(
            requestUri: $"{PlayerUri(gameId, playerId)}/bids",
            value: new {PokerHand = bid});

    async internal static Task<GetGameFlow.Result> GetGameFlow(this HttpClient client, Guid gameId)
    {
        var response = await client.GetAsync($"{GamesUri}/{gameId}");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GetGameFlow.Result>())!;
    }

    async internal static Task CheckWithSuccess(this HttpClient client, Guid gameId, Guid playerId)
    {
        var response = await client.PostAsync(requestUri: $"{PlayerUri(gameId, playerId)}/checks", content: null);
        response.EnsureSuccessStatusCode();
    }

    async internal static Task<ProblemDetails> CheckWithRuleViolation(this HttpClient client,
        Guid gameId, Guid playerId)
    {
        var response = await client.PostAsync(requestUri: $"{PlayerUri(gameId, playerId)}/checks", content: null);
        if (response.StatusCode != HttpStatusCode.BadRequest)
            throw new AssertActualExpectedException(
                expected: HttpStatusCode.BadRequest,
                actual: response.StatusCode,
                userMessage: "Expected that this call to be rejected but it unexpectedly succeeded");

        return (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
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