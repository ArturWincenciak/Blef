using System.Net;
using System.Net.Http.Json;
using Blef.Modules.Games.Application.Commands;
using Blef.Modules.Games.Application.Queries;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Microsoft.AspNetCore.Mvc;
using Xunit.Sdk;
using Deal = Blef.Modules.Games.Api.Tests.Core.ValueObjects.Deal;
using GameId = Blef.Modules.Games.Api.Tests.Core.ValueObjects.GameId;
using PlayerId = Blef.Modules.Games.Api.Tests.Core.ValueObjects.PlayerId;

namespace Blef.Modules.Games.Api.Tests.Core;

internal static class HttpApiExtensions
{
    async internal static Task<NewGame.Result> NewGame(this HttpClient client)
    {
        var response = await client.PostAsync(GamesUri, content: null);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<NewGame.Result>())!;
    }

    async internal static Task<JoinGame.Result> JoinPlayer(this HttpClient client, GameId gameId, string nick)
    {
        var response = await client.PostAsJsonAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/players",
            value: new {Nick = nick});
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<JoinGame.Result>())!;
    }

    async internal static Task<NewDeal.Result> NewDeal(this HttpClient client, GameId gameId, PlayerId playerId)
    {
        var response = await client.PostAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/players/{playerId.Id}/deals",
            content: null);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<NewDeal.Result>())!;
    }

    async internal static Task<GetPlayerCards.Result> GetCards(this HttpClient client, GameId gameId, Deal deal, PlayerId playerId)
    {
        var response = await client.GetAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/players/{playerId.Id}/deals/{deal.Number}/cards");
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
            requestUri: $"{GamesUri}/{gameId.Id}/players/{playerId.Id}/deals/{deal.Number}/bids",
            value: new {PokerHand = bid});

    async internal static Task<GetGameFlow.Result> GetGameFlow(this HttpClient client, GameId gameId)
    {
        var response = await client.GetAsync($"{GamesUri}/{gameId.Id}");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GetGameFlow.Result>())!;
    }

    async internal static Task<GetDealFlow.Result> GetDealFlow(this HttpClient client, GameId gameId, DealNumber dealNumber)
    {
        var response = await client.GetAsync($"{GamesUri}/{gameId.Id}/deals/{dealNumber.Number}");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GetDealFlow.Result>())!;
    }

    async internal static Task CheckWithSuccess(this HttpClient client, GameId gameId, Deal deal, PlayerId playerId)
    {
        var response = await client.PostAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/players/{playerId.Id}/deals/{deal.Number}/checks", content: null);
        response.EnsureSuccessStatusCode();
    }

    async internal static Task<ProblemDetails> CheckWithRuleViolation(this HttpClient client,
        GameId gameId, Deal deal, PlayerId playerId)
    {
        var response = await client.PostAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/players/{playerId.Id}/deals/{deal.Number}/checks", content: null);
        if (response.StatusCode != HttpStatusCode.BadRequest)
            throw new AssertActualExpectedException(
                HttpStatusCode.BadRequest,
                response.StatusCode,
                userMessage: "Expected that this call to be rejected but it unexpectedly succeeded");

        return (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
    }

    private static string GamesUri =>
        "games-module/games";
}