﻿using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Events;
using Blef.Shared.Abstractions.Events;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Events.Handlers;

[UsedImplicitly]
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

    public async Task Handle(BidPlaced @event, CancellationToken cancellation)
    {
        var gameplay = await _gameplaysRepository.Get(@event.Game);
        gameplay!.OnBidPlaced(@event.Deal, @event.Player, @event.PokerHand);
    }

    public async Task Handle(CheckPlaced @event, CancellationToken cancellation)
    {
        var gameplay = await _gameplaysRepository.Get(@event.Game);
        gameplay!.OnCheckPlaced(@event.Deal, @event.CheckingPlayer, @event.LooserPlayer);
    }

    public async Task Handle(DealStarted @event, CancellationToken cancellation)
    {
        var gameplay = await _gameplaysRepository.Get(@event.Game);
        gameplay!.OnDealStarted(@event.Deal, @event.Players);
    }

    public async Task Handle(GameOver @event, CancellationToken cancellation)
    {
        var gameplay = await _gameplaysRepository.Get(@event.Game);
        gameplay!.OnGameFinished(@event.Winner);
    }

    public async Task Handle(GamePlayerJoined @event, CancellationToken cancellation)
    {
        var gameplay = await _gameplaysRepository.Get(@event.Game);
        gameplay!.OnPlayerJoined(@event.Player);
    }
}