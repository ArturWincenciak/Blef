using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events.Handlers;

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

    public async Task Handle(GamePlayerJoined @event, CancellationToken cancellation) =>
        _gameplaysRepository
            .Get(@event.Game)
            .OnPlayerJoined(@event.Player);

    public async Task Handle(DealStarted @event, CancellationToken cancellation) =>
        _gameplaysRepository
            .Get(@event.Game)
            .OnDealStarted(@event.Deal, @event.Players);

    public async Task Handle(BidPlaced @event, CancellationToken cancellation) =>
        _gameplaysRepository
            .Get(@event.Game)
            .OnBidPlaced(@event.Deal, @event.Player, @event.PokerHand);

    public async Task Handle(CheckPlaced @event, CancellationToken cancellation) =>
        _gameplaysRepository
            .Get(@event.Game)
            .OnCheckPlaced(@event.Deal, @event.CheckingPlayer, @event.LooserPlayer);

    public async Task Handle(GameOver @event, CancellationToken cancellation) =>
        _gameplaysRepository
            .Get(@event.Game)
            .OnGameFinished(@event.Winner);
}