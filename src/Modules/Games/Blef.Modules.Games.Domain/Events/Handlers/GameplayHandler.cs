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
        gameplay.JoinPlayer(@event.PlayerId, @event.Nick);
    }

    public async Task Handle(DealStarted @event, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(@event.GameId);
        var dealPlayers = Map(@event.Players);
        gameplay.StartNewDeal(@event.DealNumber, dealPlayers);
    }

    public async Task Handle(BidPlaced @event, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(@event.GameId);
        gameplay.Bid(@event.DealNumber, @event.PlayerId, @event.PokerHand);
    }

    public async Task Handle(CheckPlaced @event, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(@event.GameId);
        gameplay.Check(@event.DealNumber, @event.CheckingPlayerId, @event.LooserPlayerId);
    }

    private static List<Gameplay.Deal.DealPlayer> Map(IEnumerable<DealStarted.Player> players) =>
        players.Select(player =>
            new Gameplay.Deal.DealPlayer(player.PlayerId, player.Hand.Select(card =>
                new Gameplay.Deal.DealPlayer.Card(card.FaceCard, card.Suit)).ToList())).ToList();
}