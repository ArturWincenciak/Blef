using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetDealHandler : IQueryHandler<GetDealFlow, GetDealFlow.Result>
{
    private readonly IGamesRepository _games;

    public GetDealHandler(IGamesRepository games) =>
        _games = games;

    public async Task<GetDealFlow.Result> Handle(GetDealFlow query, CancellationToken cancellation)
    {
        // todo: ...
        var game = _games.Get(query.GameId);
        var dealFlow = game.GetDealFlow(query.DealNumber);

        // temp stub
        return new GetDealFlow.Result(
            Players: new []
            {
                new GetDealFlow.Player(
                    PlayerId: Guid.NewGuid(),
                    Cards: new []
                    {
                        new GetDealFlow.Card(
                            FaceCard: "face card",
                            Suit: "suit")
                    })
            },
            Bids: new []
            {
                new GetDealFlow.DealBid(
                    Order: 1,
                    PlayerId: Guid.NewGuid(),
                    Bid: "bid")
            },
            CheckingPlayerId: Guid.NewGuid(),
            LooserPlayerId: Guid.NewGuid());
    }

    private static GetDealFlow.Card[] HideCards(IEnumerable<Card> cards) =>
        cards.Select(_ => new GetDealFlow.Card(FaceCard: "Hidden", Suit: "Hidden")).ToArray();

    private static GetDealFlow.Card[] MapCards(IEnumerable<Card> cards) =>
        cards.Select(MapCard).ToArray();

    private static GetDealFlow.Card MapCard(Card card) =>
        new(FaceCard: card.FaceCard.ToString(), Suit: card.Suit.ToString());
}