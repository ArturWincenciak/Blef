﻿using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetDealHandler : IQueryHandler<GetDeal, GetDeal.Result>
{
    private readonly IGameplaysRepository _gameplaysRepository;

    public GetDealHandler(IGameplaysRepository gameplaysRepository) =>
        _gameplaysRepository = gameplaysRepository;

    public async Task<GetDeal.Result> Handle(GetDeal query, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(query.Game);
        var dealProjection = gameplay.GetDealProjection(query.Deal);
        var result = Map(dealProjection);
        return result;
    }

    private static GetDeal.Result Map(Gameplay.DealDetails projection) =>
        new(Players: Map(projection.Players),
            Bids: Map(projection.Bids),
            DealResolution: Map(projection.DealResolution));

    private static IEnumerable<GetDeal.Player> Map(IEnumerable<DealPlayer> players) =>
        players.Select(player => new GetDeal.Player(
            PlayerId: player.Player.Id,
            Hand: Map(player.Hand.Cards)));

    private static IEnumerable<GetDeal.Card> Map(IEnumerable<Domain.ValueObjects.Cards.Card> cards) =>
        cards.Select(card => new GetDeal.Card(
            FaceCard: card.FaceCard.ToString(),
            Suit: card.Suit.ToString()));

    private static IEnumerable<GetDeal.Bid> Map(IEnumerable<Bid> bids) =>
        bids
            .Select((bid, index) => new GetDeal.Bid(
                Order: index + 1,
                PlayerId: bid.Player.Id,
                PokerHand: bid.PokerHand.Serialize()))
            .OrderBy(bid => bid.Order);

    private static GetDeal.DealResolution Map(Gameplay.DealResolution? dealResolution) =>
        new(dealResolution?.CheckingPlayer.Player.Id ?? Guid.Empty,
            dealResolution?.Looser.Player.Id ?? Guid.Empty);
}