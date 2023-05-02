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
        gameplay.StartNewDeal(@event.DealNumber);
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
}