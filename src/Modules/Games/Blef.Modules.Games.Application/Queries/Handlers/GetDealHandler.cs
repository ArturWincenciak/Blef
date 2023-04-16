using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.ValueObjects.Dto;
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
        var game = _games.Get(query.GameId);
        var dealFlow = game.GetDealFlow(new (query.GameId, query.DealNumber));
        return Map(dealFlow);
    }

    private GetDealFlow.Result Map(DealFlowResult dealFlow) =>
        new(
            Players: dealFlow.Players.Select(dealPlayer => new GetDealFlow.Player(
                dealPlayer.PlayerId.Id,
                Hand: dealPlayer.Hand.Cards.Select(c => new GetDealFlow.Card(
                    FaceCard: c.FaceCard.ToString(),
                    Suit: c.Suit.ToString())))),
            Bids: dealFlow.Bids.Select(b => new GetDealFlow.DealBid(
                b.Order,
                b.Bid.Player.Id,
                PokerHand: b.Bid.PokerHand.Serialize())),
            dealFlow.CheckingPlayerId.PlayerId,
            dealFlow.LooserPlayerId.PlayerId);
}