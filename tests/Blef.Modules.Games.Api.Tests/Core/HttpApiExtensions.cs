using System.Net.Http.Json;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;
using Blef.Modules.Games.Application.Commands;
using Blef.Modules.Games.Application.Queries;

namespace Blef.Modules.Games.Api.Tests.Core;

internal static class HttpApiExtensions
{
    private static string GamesUri =>
        "games-module/games";

    private static string GameplaysUri =>
        "games-module/gameplays";

    internal async static Task<NewGame.Result> NewGame(this HttpClient client, TestRecorder testRecorder,
        string? description = null)
    {
        var response = await client.PostAsync(GamesUri, content: null);
        response.EnsureSuccessStatusCode();
        var result = (await response.Content.ReadFromJsonAsync<NewGame.Result>())!;

        testRecorder.Record(
            request: new(GamesUri, TestRecorder.HttpMethod.Post),
            response: new(response.StatusCode, result), description);

        return result;
    }

    internal async static Task<object> JoinPlayer(this HttpClient client,
        GameId gameId, PlayerNick nick, TestRecorder testRecorder, string? description = null)
    {
        var requestUri = $"{GamesUri}/{gameId.Id}/players";
        var requestBody = new {nick.Nick};
        var response = await client.PostAsJsonAsync(requestUri, requestBody);
        var result = await DeserializeResponse<JoinGame.Result>(response);

        testRecorder.Record(
            request: new(requestUri, TestRecorder.HttpMethod.Post, requestBody),
            response: new(response.StatusCode, result), description);

        return result;
    }

    internal async static Task StartFirstDeal(this HttpClient client, GameId gameId, TestRecorder testRecorder,
        string? description = null)
    {
        var requestUri = $"{GamesUri}/{gameId.Id}/deals";
        var response = await client.PostAsync(requestUri, content: null);
        var result = await DeserializeResponse<StartFirstDeal.Result>(response);

        testRecorder.Record(
            request: new(requestUri, TestRecorder.HttpMethod.Post),
            response: new(response.StatusCode, result), description);
    }

    internal async static Task GetCards(this HttpClient client, GameId gameId,
        DealNumber deal, PlayerId playerId, TestRecorder testRecorder, string? description = null)
    {
        var requestUri = $"{GameplaysUri}/{gameId.Id}/players/{playerId.Id}/deals/{deal.Number}/cards";
        var response = await client.GetAsync(requestUri);
        var result = await DeserializeResponse<GetPlayerCards.Result>(response);

        testRecorder.Record(
            request: new(requestUri, TestRecorder.HttpMethod.Get),
            response: new(response.StatusCode, result), description);
    }

    internal async static Task BidHighCard(this HttpClient client,
        GameId gameId, PlayerId playerId, FaceCard faceCard, TestRecorder testRecorder, string? description = null) =>
        await Bid(client, gameId, playerId,
            pokerHand: "high-card",
            requestBody: new
            {
                FaceCard = faceCard.ToString()
            }, testRecorder, description);

    internal async static Task BidPair(this HttpClient client,
        GameId gameId, PlayerId playerId, FaceCard faceCard, TestRecorder testRecorder, string? description = null) =>
        await Bid(client, gameId, playerId,
            pokerHand: "pair",
            requestBody: new
            {
                FaceCard = faceCard.ToString()
            }, testRecorder, description);

    internal async static Task BidTwoPairs(this HttpClient client, GameId gameId,
        PlayerId playerId, FaceCard first, FaceCard second, TestRecorder testRecorder, string? description) =>
        await Bid(client, gameId, playerId,
            pokerHand: "two-pairs",
            requestBody: new
            {
                FirstFaceCard = first.ToString(),
                SecondFaceCard = second.ToString()
            }, testRecorder, description);

    internal async static Task BidLowStraight(this HttpClient client, GameId gameId,
        PlayerId playerId, TestRecorder testRecorder, string? description) =>
        await Bid(client, gameId, playerId,
            pokerHand: "low-straight",
            requestBody: new { },
            testRecorder, description);

    internal async static Task BidHighStraight(this HttpClient client, GameId gameId,
        PlayerId playerId, TestRecorder testRecorder, string? description) =>
        await Bid(client, gameId, playerId,
            pokerHand: "high-straight",
            requestBody: new { },
            testRecorder, description);

    internal async static Task BidThreeOfAKind(this HttpClient client, GameId gameId,
        PlayerId playerId, FaceCard faceCard, TestRecorder testRecorder, string? description = null) =>
        await Bid(client, gameId, playerId,
            pokerHand: "three-of-a-kind",
            requestBody: new
            {
                FaceCard = faceCard.ToString()
            }, testRecorder, description);

    internal async static Task BidFullHouse(this HttpClient client, GameId gameId, PlayerId playerId,
        FaceCard threeOfAKind, FaceCard pair, TestRecorder testRecorder, string? description = null) =>
        await Bid(client, gameId, playerId,
            pokerHand: "full-house",
            requestBody: new
            {
                ThreeOfAKind = threeOfAKind.ToString(),
                Pair = pair.ToString()
            }, testRecorder, description);

    internal async static Task BidFlush(this HttpClient client, GameId gameId,
        PlayerId playerId, Suit suit, TestRecorder testRecorder, string? description = null) =>
        await Bid(client, gameId, playerId,
            pokerHand: "flush",
            requestBody: new
            {
                Suit = suit.ToString()
            }, testRecorder, description);

    internal async static Task BidFourOfAKind(this HttpClient client, GameId gameId,
        PlayerId playerId, FaceCard faceCard, TestRecorder testRecorder, string? description) =>
        await Bid(client, gameId, playerId,
            pokerHand: "four-of-a-kind",
            requestBody: new
            {
                FaceCard = faceCard.ToString()
            }, testRecorder, description);

    internal async static Task BidStraightFlush(this HttpClient client, GameId gameId,
        PlayerId playerId, Suit suit, TestRecorder testRecorder, string? description = null) =>
        await Bid(client, gameId, playerId,
            pokerHand: "straight-flush",
            requestBody: new
            {
                Suit = suit.ToString()
            }, testRecorder, description);

    internal async static Task BidRoyalFlush(this HttpClient client,
        GameId gameId, PlayerId playerId, Suit suit, TestRecorder testRecorder, string? description) =>
        await Bid(client, gameId, playerId,
            pokerHand: "royal-flush",
            requestBody: new
            {
                Suit = suit.ToString()
            }, testRecorder, description);

    private async static Task Bid<TValue>(this HttpClient client,
        GameId gameId, PlayerId playerId, string pokerHand, TValue requestBody, TestRecorder testRecorder,
        string? description = null)
    {
        var requestUri = $"{GamesUri}/{gameId.Id}/players/{playerId.Id}/bids/{pokerHand}";
        var response = await client.PostAsJsonAsync(requestUri, requestBody);
        var result = await DeserializeResponse(response);

        testRecorder.Record(
            request: new(requestUri, TestRecorder.HttpMethod.Post, requestBody),
            response: new(response.StatusCode, result), description);
    }

    internal async static Task GetGameFlow(this HttpClient client, GameId gameId, TestRecorder testRecorder,
        string? description = null)
    {
        var requestUri = $"{GameplaysUri}/{gameId.Id}";
        var response = await client.GetAsync(requestUri);
        var result = await DeserializeResponse<GetGame.Result>(response);

        testRecorder.Record(
            request: new(requestUri, TestRecorder.HttpMethod.Get),
            response: new(response.StatusCode, result), description);
    }

    internal async static Task GetDealFlow(this HttpClient client, GameId gameId,
        DealNumber dealNumber, TestRecorder testRecorder, string? description = null)
    {
        var requestUri = $"{GameplaysUri}/{gameId.Id}/deals/{dealNumber.Number}";
        var response = await client.GetAsync(requestUri);
        var result = await DeserializeResponse<GetDeal.Result>(response);

        testRecorder.Record(
            request: new(requestUri, TestRecorder.HttpMethod.Get),
            response: new(response.StatusCode, result), description);
    }

    internal async static Task Check(this HttpClient client,
        GameId gameId, PlayerId playerId, TestRecorder testRecorder, string? description = null)
    {
        var requestUri = $"{GamesUri}/{gameId.Id}/players/{playerId.Id}/checks";
        var response = await client.PostAsync(requestUri, content: null);
        var result = await DeserializeResponse(response);

        testRecorder.Record(
            request: new(requestUri, TestRecorder.HttpMethod.Post),
            response: new(response.StatusCode, result), description);
    }

    internal async static Task Home(this HttpClient client, TestRecorder testRecorder)
    {
        const string requestUri = "/games-module/home";
        var response = await client.GetAsync(requestUri);
        var result = await DeserializeResponse<HomeResponse>(response);

        testRecorder.Record(
            request: new(requestUri, TestRecorder.HttpMethod.Get),
            response: new(response.StatusCode, result));
    }

    private async static Task<object> DeserializeResponse(HttpResponseMessage response)
    {
        object result = response.IsSuccessStatusCode switch
        {
            true => "__without_body__",
            false => (await response.Content.ReadFromJsonAsync<ProblemDetails>())!
        };
        return result;
    }

    private async static Task<object> DeserializeResponse<TSuccessModel>(HttpResponseMessage response)
        where TSuccessModel : class
    {
        object result = response.IsSuccessStatusCode switch
        {
            true => (await response.Content.ReadFromJsonAsync<TSuccessModel>())!,
            false => (await response.Content.ReadFromJsonAsync<ProblemDetails>())!
        };
        return result;
    }
}