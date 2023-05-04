using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.ValueObjects;
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
        var gameplay = _gameplaysRepository.Get(@event.Game.Id);
        gameplay.OnPlayerJoined(@event.Player.Id, @event.Nick.Nick);
    }

    public async Task Handle(DealStarted @event, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(@event.Game.Id);
        var dealPlayers = Map(@event.Players);
        gameplay.OnDealStarted(@event.Deal.Number, dealPlayers);
    }

    public async Task Handle(BidPlaced @event, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(@event.Game.Id);
        gameplay.OnBidPlaced(@event.Deal.Number, @event.Player.Id, @event.PokerHand.Serialize());
    }

    public async Task Handle(CheckPlaced @event, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(@event.Game.Id);
        gameplay.OnCheckPlaced(@event.Deal.Number, @event.CheckingPlayer.Id, @event.LooserPlayer.Player.Id);
    }

    private static List<GameplayProjection.DealPlayer> Map(IEnumerable<DealPlayer> players) =>
        players.Select(player =>
            new GameplayProjection.DealPlayer(player.Player.Id, player.Hand.Cards.Select(card =>
                new GameplayProjection.Card(card.FaceCard.ToString(), card.Suit.ToString())).ToList())).ToList();
}