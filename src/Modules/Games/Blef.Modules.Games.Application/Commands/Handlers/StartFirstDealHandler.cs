﻿using Blef.Modules.Games.Application.Exceptions;
using Blef.Modules.Games.Application.Repositories;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Events;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class StartFirstDealHandler : ICommandHandler<StartFirstDeal, StartFirstDeal.Result>
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly IGamesRepository _games;

    public StartFirstDealHandler(IGamesRepository games, IDomainEventDispatcher domainEventDispatcher)
    {
        _games = games;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task<StartFirstDeal.Result> Handle(StartFirstDeal command, CancellationToken cancellation)
    {
        var game = await _games.Get(new(command.GameId));
        if (game is null)
            throw new GameNotFoundException(command.GameId);

        var newDealStarted = game.StartFirstDeal();
        await _domainEventDispatcher.Dispatch(newDealStarted, cancellation);
        return new(newDealStarted.Deal.Number);
    }
}