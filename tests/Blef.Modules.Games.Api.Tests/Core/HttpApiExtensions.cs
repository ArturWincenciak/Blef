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

    async internal static Task<JoinGame.Result> JoinPlayer(this HttpClient client, GameId gameId, PlayerNick nick)
    {
        var response = await client.PostAsJsonAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/players",
            value: new {nick.Nick});
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<JoinGame.Result>())!;
    }

    async internal static Task<StartFirstDeal.Result> StartFirstDeal(this HttpClient client, GameId gameId)
    {
        var response = await client.PostAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/deals",
            content: null);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<StartFirstDeal.Result>())!;
    }

    async internal static Task<GetPlayerCards.Result> GetCards(this HttpClient client,
        GameId gameId, DealNumber deal, PlayerId playerId)
    {
        var response = await client.GetAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/players/{playerId.Id}/deals/{deal.Number}/cards");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<GetPlayerCards.Result>())!;
    }

    async internal static Task<object> BidWithSuccess(this HttpClient client, GameId gameId, PlayerId playerId, string bid)
    {
        var response = await Bid(client, gameId, playerId, bid);
        response.EnsureSuccessStatusCode();
        return Success;
    }

    private async static Task<HttpResponseMessage> Bid(HttpClient client,
        GameId gameId, PlayerId playerId, string bid) =>
        await client.PostAsJsonAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/players/{playerId.Id}/bids",
            value: new {PokerHand = bid});

    private async static Task<object> Bid<TValue>(this HttpClient client,
        GameId gameId, PlayerId playerId, string pokerHand, TValue value)
    {
        var response = await client.PostAsJsonAsync(
            requestUri: $"{GamesUri}/{gameId.Id}/players/{playerId.Id}/bids/{pokerHand}",
            value: value);

        if (response.IsSuccessStatusCode)
            return Success;

        return (await response.Content.ReadFromJsonAsync<ProblemDetails>())!;
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
