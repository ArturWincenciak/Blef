using System.Net;
using System.Net.Http.Json;
using Blef.Modules.Games.Api.Tests.Core.ValueObjects;
using Blef.Modules.Games.Application.Queries;
using Blef.Modules.Games.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Xunit.Sdk;
using Deal = Blef.Modules.Games.Api.Tests.Core.ValueObjects.Deal;

namespace Blef.Modules.Games.Api.Tests.Core;

internal static class HttpApiExtensions
{
    async internal static Task<GameId> MakeNewGame(this HttpClient client)
    {
        var response = await client.PostAsync(GamesUri, content: null);
        response.EnsureSuccessStatusCode();
        var game = await response.Content.ReadFromJsonAsync<Dto.Game>();
        return new (game!.GameId);
    }

    async internal static Task<Guid> JoinPlayer(this HttpClient client, GameId gameId, string nick)
    {
        var response = await client.PostAsJsonAsync(
            requestUri: $"{GamePlayersUri(gameId)}",
            value: new {Nick = nick});
        response.EnsureSuccessStatusCode();
        var player = await response.Content.ReadFromJsonAsync<Dto.Player>();
        return player!.PlayerId;
    }

    async internal static Task Deal(this HttpClient client, GameId gameId, PlayerId playerId)
    {
        var response = await client.PostAsync(
            requestUri: $"{DealsUri(gameId)}/players/{playerId}",
            content: null);
        response.EnsureSuccessStatusCode();
        //todo: use deal result content
    }

    async internal static Task<GetPlayerCards.Result> GetCards(this HttpClient client, GameId gameId, Deal deal, PlayerId playerId)
    {
        var response = await client.GetAsync(
            requestUri: $"{DealPlayerUri(gameId, deal, playerId)}/cards");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GetPlayerCards.Result>())!;
    }

    async internal static Task BidWithSuccess(this HttpClient client, GameId gameId, Deal deal, PlayerId playerId, string bid)
    {
        var response = await Bid(client, gameId, deal, playerId, bid);
        response.EnsureSuccessStatusCode();
    }

    async internal static Task<ProblemDetails> BidWithRuleViolation(this HttpClient client,
        GameId gameId, Deal deal, PlayerId playerId, string bid)
    {
        var response = await Bid(client, gameId, deal, playerId, bid);
        if (response.StatusCode != HttpStatusCode.BadRequest)
            throw new AssertActualExpectedException(
                HttpStatusCode.BadRequest,
                response.StatusCode,
                userMessage: "Expected that this call to be rejected but it unexpectedly succeeded");

        return (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
    }

    private async static Task<HttpResponseMessage> Bid(HttpClient client, GameId gameId, Deal deal, PlayerId playerId, string bid) =>
        await client.PostAsJsonAsync(
            requestUri: $"{DealPlayerUri(gameId, deal, playerId)}/bids",
            value: new {PokerHand = bid});

    async internal static Task<GetGameFlow.Result> GetGameFlow(this HttpClient client, GameId gameId)
    {
        var response = await client.GetAsync($"{GamesUri}/{gameId}");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GetGameFlow.Result>())!;
    }

    async internal static Task CheckWithSuccess(this HttpClient client, GameId gameId, Deal deal, PlayerId playerId)
    {
        var response = await client.PostAsync(requestUri: $"{DealPlayerUri(gameId, deal, playerId)}/checks", content: null);
        response.EnsureSuccessStatusCode();
    }

    async internal static Task<ProblemDetails> CheckWithRuleViolation(this HttpClient client,
        GameId gameId, Deal deal, PlayerId playerId)
    {
        var response = await client.PostAsync(requestUri: $"{DealPlayerUri(gameId, deal, playerId)}/checks", content: null);
        if (response.StatusCode != HttpStatusCode.BadRequest)
            throw new AssertActualExpectedException(
                HttpStatusCode.BadRequest,
                response.StatusCode,
                userMessage: "Expected that this call to be rejected but it unexpectedly succeeded");

        return (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
    }

    private static string GamesUri =>
        "games-module/games";

    private static string GameUri(GameId gameId) =>
        $"{GamesUri}/{gameId.Id}";

    private static string GamePlayersUri(GameId gameId) =>
        $"{GameUri(gameId)}/players";

    private static string DealsUri(GameId gameId) =>
        $"{GameUri(gameId)}/deals";

    private static string DealUri(GameId gameId, Deal deal) =>
        $"{DealsUri(gameId)}/{deal.Number}";

    private static string DealPlayerUri(GameId gameId, Deal deal, PlayerId playerId) =>
        $"{DealUri(gameId, deal)}/players/{playerId.Id}";

    private static class Dto
    {
        internal record Game(Guid GameId);

        internal record Deal(Guid GameId, int DealId);

        internal record Player(Guid PlayerId);
    }
}