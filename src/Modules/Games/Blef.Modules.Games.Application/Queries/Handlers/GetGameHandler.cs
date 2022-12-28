using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Queries.Handlers;

internal sealed class GetGameHandler : IQueryHandler<GetGameFlow, GetGameFlow.Result>
{
    private readonly IGamesRepository _games;

    public GetGameHandler(IGamesRepository games)
    {
        _games = games;
    }

    public Task<GetGameFlow.Result> Handle(GetGameFlow query, CancellationToken cancellation)
    {
        var game = _games.Get(query.GameId);
        var gameFlow = game.GetFlow();

        var bidFlow = gameFlow.Bids
            .Select(bid => new GetGameFlow.GameBid(
                Order: bid.Order,
                PlayerId: bid.PlayerId,
                Bid: bid.Bid))
            .ToArray();

        var gameIsInProgress = gameFlow.LooserPlayerId == Guid.Empty;

        var players = gameFlow.Players
            .Select(player => new GetGameFlow.Player(
                Id: player.PlayerId,
                Nick: player.Nick,
                Cards: gameIsInProgress
                    ? HideCards(player.Cards)
                    : MapCards(cards: player.Cards)))
            .ToArray();

        var result = new GetGameFlow.Result(players, bidFlow, gameFlow.CheckingPlayerId, gameFlow.LooserPlayerId);
        return Task.FromResult(result);
    }

    private static GetGameFlow.Card[] HideCards(IEnumerable<Card> cards) =>
        cards.Select(_ => new GetGameFlow.Card(FaceCard: "Hidden", Suit: "Hidden")).ToArray();

    private static GetGameFlow.Card[] MapCards(IEnumerable<Card> cards) =>
        cards.Select(MapCard).ToArray();

    private static GetGameFlow.Card MapCard(Card card) =>
        new(card.FaceCard.ToString(), card.Suit.ToString());
}