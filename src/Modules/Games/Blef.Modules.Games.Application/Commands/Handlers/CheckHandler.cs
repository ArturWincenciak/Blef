using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Events;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Events;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class CheckHandler : ICommandHandler<Check>
{
    private readonly IGamesRepository _games;
    private readonly IDomainEventDispatcher _eventDispatcher;

    public CheckHandler(IGamesRepository games, IDomainEventDispatcher eventDispatcher)
    {
        _games = games;
        _eventDispatcher = eventDispatcher;
    }

    public async Task Handle(Check command, CancellationToken cancellation)
    {
        var game = _games.Get(command.GameId);
        var events = game.Check(new (command.GameId, command.DealNumber), command.PlayerId);

        foreach (var @event in events)
            await Dispatch(@event, cancellation);

        // todo: return looser player
        // todo: return next deal if created
        // todo: return in header next possible actions
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
        }
    }
}