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
        var game = await response.Content.ReadFromJsonAsync<Dto.Game>();
        return game!.GameId;
    }

    async internal static Task<Guid> JoinPlayer(this HttpClient client, Guid gameId, string nick)
    {
        var response = await client.PostAsJsonAsync(
            requestUri: $"{GamePlayersUri(gameId)}",
            value: new {Nick = nick});
        response.EnsureSuccessStatusCode();
        var player = await response.Content.ReadFromJsonAsync<Dto.Player>();
        return player!.PlayerId;
    }

    async internal static Task Deal(this HttpClient client, Guid gameId, Guid playerId)
    {
        var response = await client.PostAsync(
            requestUri: $"{DealsUri(gameId)}/players/{playerId}",
            content: null);
        response.EnsureSuccessStatusCode();
        //todo: use deal result content
    }

    async internal static Task<GetPlayerCards.Result> GetCards(this HttpClient client, Guid gameId, int deal, Guid playerId)
    {
        var response = await client.GetAsync(
            requestUri: $"{DealPlayerUri(gameId, deal, playerId)}/cards");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GetPlayerCards.Result>())!;
    }

    async internal static Task BidWithSuccess(this HttpClient client, Guid gameId, int deal, Guid playerId, string bid)
    {
        var response = await Bid(client, gameId, deal, playerId, bid);
        response.EnsureSuccessStatusCode();
    }

    async internal static Task<ProblemDetails> BidWithRuleViolation(this HttpClient client,
        Guid gameId, int deal, Guid playerId, string bid)
    {
        var response = await Bid(client, gameId, deal, playerId, bid);
        if (response.StatusCode != HttpStatusCode.BadRequest)
            throw new AssertActualExpectedException(
                HttpStatusCode.BadRequest,
                response.StatusCode,
                userMessage: "Expected that this call to be rejected but it unexpectedly succeeded");

        return (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
    }

    private async static Task<HttpResponseMessage> Bid(HttpClient client, Guid gameId, int deal, Guid playerId, string bid) =>
        await client.PostAsJsonAsync(
            requestUri: $"{DealPlayerUri(gameId, deal, playerId)}/bids",
            value: new {PokerHand = bid});

    async internal static Task<GetGameFlow.Result> GetGameFlow(this HttpClient client, Guid gameId)
    {
        var response = await client.GetAsync($"{GamesUri}/{gameId}");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GetGameFlow.Result>())!;
    }

    async internal static Task CheckWithSuccess(this HttpClient client, Guid gameId, Guid playerId)
    {
        var response = await client.PostAsync(requestUri: $"{GamePlayerUri(gameId, playerId)}/checks", content: null);
        response.EnsureSuccessStatusCode();
    }

    async internal static Task<ProblemDetails> CheckWithRuleViolation(this HttpClient client,
        Guid gameId, Guid playerId)
    {
        var response = await client.PostAsync(requestUri: $"{GamePlayerUri(gameId, playerId)}/checks", content: null);
        if (response.StatusCode != HttpStatusCode.BadRequest)
            throw new AssertActualExpectedException(
                HttpStatusCode.BadRequest,
                response.StatusCode,
                userMessage: "Expected that this call to be rejected but it unexpectedly succeeded");

        return (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
    }

    private static string GamesUri =>
        "games-module/games";

    private static string GameUri(Guid gameId) =>
        $"{GamesUri}/{gameId}";

    private static string GamePlayersUri(Guid gameId) =>
        $"{GameUri(gameId)}/players";

    private static string GamePlayerUri(Guid gameId, Guid playerId) =>
        $"{GamePlayersUri(gameId)}/{playerId}";

    private static string DealsUri(Guid gameId) =>
        $"{GameUri(gameId)}/deals";

    private static string DealUri(Guid gameId, int deal) =>
        $"{DealsUri(gameId)}/{deal}";

    private static string DealPlayerUri(Guid gameId, int deal, Guid playerId) =>
        $"{DealUri(gameId, deal)}/players/{playerId}";

    private static class Dto
    {
        internal record Game(Guid GameId);

        internal record Deal(Guid GameId, int DealId);

        internal record Player(Guid PlayerId);
    }
}