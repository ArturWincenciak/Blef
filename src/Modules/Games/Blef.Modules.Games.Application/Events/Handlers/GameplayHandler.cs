using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Events;
using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Application.Events.Handlers;

internal sealed class GameplayHandler :
    IDomainEventHandler<GamePlayerJoined>,
    IDomainEventHandler<DealStarted>,
    IDomainEventHandler<BidPlaced>,
    IDomainEventHandler<CheckPlaced>,
    IDomainEventHandler<GameOver>
{
    private readonly IGameplaysRepository _gameplaysRepository;

    public GameplayHandler(IGameplaysRepository gameplaysRepository) =>
        _gameplaysRepository = gameplaysRepository;

    public Task Handle(BidPlaced @event, CancellationToken cancellation)
    {
        _gameplaysRepository
            .Get(@event.Game)
            .OnBidPlaced(@event.Deal, @event.Player, @event.PokerHand);
        return Task.CompletedTask;
    }

    public Task Handle(CheckPlaced @event, CancellationToken cancellation)
    {
        _gameplaysRepository
            .Get(@event.Game)
            .OnCheckPlaced(@event.Deal, @event.CheckingPlayer, @event.LooserPlayer);
        return Task.CompletedTask;
    }

    public Task Handle(DealStarted @event, CancellationToken cancellation)
    {
        _gameplaysRepository
            .Get(@event.Game)
            .OnDealStarted(@event.Deal, @event.Players);
        return Task.CompletedTask;
    }

    public Task Handle(GameOver @event, CancellationToken cancellation)
    {
        _gameplaysRepository
            .Get(@event.Game)
            .OnGameFinished(@event.Winner);
        return Task.CompletedTask;
    }

    public Task Handle(GamePlayerJoined @event, CancellationToken cancellation)
    {
        _gameplaysRepository
            .Get(@event.Game)
            .OnPlayerJoined(@event.Player);
        return Task.CompletedTask;
    }
}