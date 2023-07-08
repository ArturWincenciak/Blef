using System.Net.Http.Json;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;
using Blef.Modules.Games.Application.Commands;
using Blef.Modules.Games.Application.Queries;

namespace Blef.Modules.Games.Api.Tests.Core;

internal static class HttpApiExtensions
{
    private readonly static TestRecorder.EmptyCommandResult Success = new("Success");

    private static string GamesUri =>
        "games-module/games";

    private static string GameplaysUri =>
        "games-module/gameplays";

    async internal static Task<NewGame.Result> NewGame(this HttpClient client)
    {
        var response = await client.PostAsync(GamesUri, content: null);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<NewGame.Result>())!;
    }

    async internal static Task<object> JoinPlayer(this HttpClient client, GameId gameId, PlayerNick nick)
    {
        var response = await client.PostAsJsonAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/players",
            value: new {nick.Nick});

        if(response.IsSuccessStatusCode)
            return (await response.Content.ReadFromJsonAsync<JoinGame.Result>())!;

        return (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
    }

    async internal static Task<object> StartFirstDeal(this HttpClient client, GameId gameId)
    {
        var response = await client.PostAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/deals",
            content: null);

        if(response.IsSuccessStatusCode)
            return (await response.Content.ReadFromJsonAsync<StartFirstDeal.Result>())!;

        return (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
    }

    async internal static Task<GetPlayerCards.Result> GetCards(this HttpClient client,
        GameId gameId, DealNumber deal, PlayerId playerId)
    {
        var response = await client.GetAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/players/{playerId.Id}/deals/{deal.Number}/cards");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GetPlayerCards.Result>())!;
    }

    async internal static Task<object> BidHighCard(this HttpClient client,
        GameId gameId, PlayerId playerId, FaceCard faceCard) =>
        await Bid(client, gameId, playerId, pokerHand: "high-card",
            value: new
            {
                FaceCard = faceCard.ToString()
            });

    async internal static Task<object> BidPair(this HttpClient client,
        GameId gameId, PlayerId playerId, FaceCard faceCard) =>
        await Bid(client, gameId, playerId, pokerHand: "pair",
            value: new
            {
                FaceCard = faceCard.ToString()
            });

    async internal static Task<object> BidTwoPairs(this HttpClient client,
        GameId gameId, PlayerId playerId, FaceCard first, FaceCard second) =>
        await Bid(client, gameId, playerId, pokerHand: "two-pairs",
            value: new
            {
                FirstFaceCard = first.ToString(),
                SecondFaceCard = second.ToString()
            });

    async internal static Task<object> BidLowStraight(this HttpClient client,
        GameId gameId, PlayerId playerId) =>
        await Bid(client, gameId, playerId, pokerHand: "low-straight", value: new { });

    async internal static Task<object> BidHighStraight(this HttpClient client,
        GameId gameId, PlayerId playerId) =>
        await Bid(client, gameId, playerId, pokerHand: "high-straight", value: new { });

    async internal static Task<object> BidThreeOfAKind(this HttpClient client,
        GameId gameId, PlayerId playerId, FaceCard faceCard) =>
        await Bid(client, gameId, playerId, pokerHand: "three-of-a-kind",
            value: new
            {
                FaceCard = faceCard.ToString()
            });

    async internal static Task<object> BidFullHouse(this HttpClient client,
        GameId gameId, PlayerId playerId, FaceCard threeOfAKind, FaceCard pair) =>
        await Bid(client, gameId, playerId, pokerHand: "full-house",
            value: new
            {
                ThreeOfAKind = threeOfAKind.ToString(),
                Pair = pair.ToString()
            });

    async internal static Task<object> BidFlush(this HttpClient client,
        GameId gameId, PlayerId playerId, Suit suit) =>
        await Bid(client, gameId, playerId, pokerHand: "flush",
            value: new
            {
                Suit = suit.ToString()
            });

    async internal static Task<object> BidFourOfAKind(this HttpClient client,
        GameId gameId, PlayerId playerId, FaceCard faceCard) =>
        await Bid(client, gameId, playerId, pokerHand: "four-of-a-kind",
            value: new
            {
                FaceCard = faceCard.ToString()
            });

    async internal static Task<object> BidStraightFlush(this HttpClient client,
        GameId gameId, PlayerId playerId, Suit suit) =>
        await Bid(client, gameId, playerId, pokerHand: "straight-flush",
            value: new
            {
                Suit = suit.ToString()
            });

    async internal static Task<object> BidRoyalFlush(this HttpClient client,
        GameId gameId, PlayerId playerId, Suit suit) =>
        await Bid(client, gameId, playerId, pokerHand: "royal-flush",
            value: new
            {
                Suit = suit.ToString()
            });

    private async static Task<object> Bid<TValue>(this HttpClient client,
        GameId gameId, PlayerId playerId, string pokerHand, TValue value)
    {
        var response = await client.PostAsJsonAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/players/{playerId.Id}/bids/{pokerHand}",
            value);

        if (response.IsSuccessStatusCode)
            return Success;

        return (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
    }

    async internal static Task<GetGame.Result> GetGameFlow(this HttpClient client, GameId gameId)
    {
        var response = await client.GetAsync($"{GameplaysUri}/{gameId.Id}");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GetGame.Result>())!;
    }

    async internal static Task<GetDeal.Result> GetDealFlow(this HttpClient client, GameId gameId,
        DealNumber dealNumber)
    {
        var response = await client.GetAsync($"{GameplaysUri}/{gameId.Id}/deals/{dealNumber.Number}");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GetDeal.Result>())!;
    }

    async internal static Task<object> CheckWithSuccess(this HttpClient client, GameId gameId, PlayerId playerId)
    {
        var response = await client.PostAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/players/{playerId.Id}/checks", content: null);
        response.EnsureSuccessStatusCode();
        return Success;
    }
}