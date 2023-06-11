using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Events;
using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Events;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class CheckHandler : ICommandHandler<Check>
{
    private readonly IDomainEventDispatcher _eventDispatcher;
    private readonly IGamesRepository _games;

    public CheckHandler(IGamesRepository games, IDomainEventDispatcher eventDispatcher)
    {
        _games = games;
        _eventDispatcher = eventDispatcher;
    }

    public async Task Handle(Check command, CancellationToken cancellation)
    {
        var game = await _games.Get(new GameId(command.GameId));
        var events = game.Check(new CheckingPlayer(new PlayerId(command.PlayerId)));

        foreach (var @event in events)
            await Dispatch(@event, cancellation);
    }

    private async Task Dispatch(IDomainEvent @event, CancellationToken cancellation)
    {
        switch (@event)
        {
            case CheckPlaced checkPlaced:
                await _eventDispatcher.Dispatch(checkPlaced, cancellation);
                break;
            case DealStarted dealStarted:
                await _eventDispatcher.Dispatch(dealStarted, cancellation);
                break;
            case GameOver gameFinished:
                await _eventDispatcher.Dispatch(gameFinished, cancellation);
                break;
        }
    }
}