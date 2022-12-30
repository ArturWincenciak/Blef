using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetGameHandler : IQueryHandler<GetGameFlow, GetGameFlow.Result>
{
    private readonly IGamesRepository _games;

    public GetGameHandler(IGamesRepository games) =>
        _games = games;

    public Task<GetGameFlow.Result> Handle(GetGameFlow query, CancellationToken cancellation)
    {
        var game = _games.Get(query.GameId);
        var gameFlow = game.GetFlow();

        var bidFlow = gameFlow.Bids
            .Select(bid => new GetGameFlow.GameBid(
                bid.Order,
                bid.PlayerId,
                bid.Bid))
            .ToArray();

        var gameIsInProgress = gameFlow.LooserPlayerId == Guid.Empty;

        var players = gameFlow.Players
            .Select(player => new GetGameFlow.Player(
                player.PlayerId,
                player.Nick,
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
        new(FaceCard: card.FaceCard.ToString(), Suit: card.Suit.ToString());
}