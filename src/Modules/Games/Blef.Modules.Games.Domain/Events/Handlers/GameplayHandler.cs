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
        var gameplay = _gameplaysRepository.Get(@event.Game);
        gameplay.OnPlayerJoined(@event.Player);
    }

    public async Task Handle(DealStarted @event, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(@event.Game);
        gameplay.OnDealStarted(@event.Deal, @event.Players);
    }

    public async Task Handle(BidPlaced @event, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(@event.Game);
        gameplay.OnBidPlaced(@event.Deal, @event.Player, @event.PokerHand);
    }

    public async Task Handle(CheckPlaced @event, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(@event.Game);
        gameplay.OnCheckPlaced(@event.Deal, @event.CheckingPlayer, @event.LooserPlayer);
    }
}