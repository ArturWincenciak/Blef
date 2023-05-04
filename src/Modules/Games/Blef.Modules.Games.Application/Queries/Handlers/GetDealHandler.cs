using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetDealHandler : IQueryHandler<GetDeal, GetDeal.Result>
{
    private readonly IGameplaysRepository _gameplaysRepository;

    public GetDealHandler(IGameplaysRepository gameplaysRepository) =>
        _gameplaysRepository = gameplaysRepository;

    public async Task<GetDeal.Result> Handle(GetDeal query, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(query.GameId.Id);
        var deal = gameplay.GetDealProjection(query.DealNumber.Number);
        var result = Map(deal);
        return result;
    }

    private static GetDeal.Result Map(GameplayProjection.DealProjection projection) =>
        new GetDeal.Result(
            Players: Map(projection.Players),
            Bids: Map(projection.Bids),
            CheckingPlayerId: projection.CheckingPlayerId,
            LooserPlayerId: projection.LooserPlayerId);

    private static IEnumerable<GetDeal.Player> Map(IEnumerable<GameplayProjection.DealPlayer> players) =>
        players.Select(player => new GetDeal.Player(
            PlayerId: player.PlayerId,
            Hand: Map(player.Hand)));

    private static IEnumerable<GetDeal.Bid> Map(IEnumerable<GameplayProjection.Bid> bids) =>
        bids
            .Select(bid => new GetDeal.Bid(
                Order: bid.Order,
                PlayerId: bid.PlayerId,
                PokerHand: bid.PokerHand))
            .OrderBy(bid => bid.Order);

    private static IEnumerable<GetDeal.Card> Map(IEnumerable<GameplayProjection.Card> cards) =>
        cards.Select(card => new GetDeal.Card(
            FaceCard: card.FaceCard,
            Suit: card.Suit));
}