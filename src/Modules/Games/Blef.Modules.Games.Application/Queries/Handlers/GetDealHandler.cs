using Blef.Modules.Games.Application.Exceptions;
using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Model;
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
        var gameplay = await _gameplaysRepository.Get(new GameId(query.GameId));
        if (gameplay is null)
            throw new GameNotFoundException(query.GameId);

        var deal = gameplay.GetDealProjection(new DealNumber(query.DealNumber));
        if (deal is null)
            throw new DealNotFoundException(query.GameId, query.DealNumber);

        return Map(deal);
    }

    private static GetDeal.Result Map(Gameplay.DealDetails projection) =>
        new(Players: Map(projection.Players),
            Bids: Map(projection.Bids),
            DealResolution: Map(projection.DealResolution));

    private static IReadOnlyCollection<GetDeal.Player> Map(IEnumerable<DealPlayer> players) =>
        players
            .Select(player => new GetDeal.Player(
                player.Player.Id,
                Hand: Map(player.Hand.Cards)))
            .ToArray();

    private static IReadOnlyCollection<GetDeal.Card> Map(IEnumerable<Card> cards) =>
        cards
            .Select(card => new GetDeal.Card(
                FaceCard: card.FaceCard.ToString(),
                Suit: card.Suit.ToString()))
            .ToArray();

    private static IReadOnlyCollection<GetDeal.Bid> Map(IEnumerable<Gameplay.BidRecord> bids) =>
        bids
            .Select(bidRecord => new GetDeal.Bid(
                bidRecord.Order,
                bidRecord.Bid.Player.Id,
                PokerHand: bidRecord.Bid.PokerHand.Serialize()))
            .OrderBy(bid => bid.Order)
            .ToArray();

    private static GetDeal.DealResolution Map(Gameplay.DealResolution? dealResolution) =>
        new(CheckingPlayerId: dealResolution?.CheckingPlayer.Player.Id ?? Guid.Empty,
            LooserPlayerId: dealResolution?.Looser.Player.Id ?? Guid.Empty);
}