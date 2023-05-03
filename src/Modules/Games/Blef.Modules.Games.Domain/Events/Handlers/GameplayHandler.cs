using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events.Handlers;

internal sealed class GameplayHandler :
    IDomainEventHandler<GamePlayerJoined>,
    IDomainEventHandler<DealStarted>,
    IDomainEventHandler<BidPlaced>,
    IDomainEventHandler<CheckPlaced>
{
    private readonly IGameplaysRepository _gameplaysRepository;

    public GameplayHandler(IGameplaysRepository gameplaysRepository) =>
        _gameplaysRepository = gameplaysRepository;

    public async Task Handle(GamePlayerJoined @event, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(@event.GameId);
        gameplay.OnPlayerJoined(@event.PlayerId, @event.Nick);
    }

    public async Task Handle(DealStarted @event, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(@event.GameId);
        var dealPlayers = Map(@event.Players);
        gameplay.OnDealStarted(@event.DealNumber, dealPlayers);
    }

    public async Task Handle(BidPlaced @event, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(@event.GameId);
        gameplay.OnBidPlaced(@event.DealNumber, @event.PlayerId, @event.PokerHand);
    }

    public async Task Handle(CheckPlaced @event, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(@event.GameId);
        gameplay.OnCheckPlaced(@event.DealNumber, @event.CheckingPlayerId, @event.LooserPlayerId);
    }

    private static List<GameplayProjection.DealPlayer> Map(IEnumerable<DealStarted.Player> players) =>
        players.Select(player =>
            new GameplayProjection.DealPlayer(player.PlayerId, player.Hand.Select(card =>
                new GameplayProjection.Card(card.FaceCard, card.Suit)).ToList())).ToList();
}