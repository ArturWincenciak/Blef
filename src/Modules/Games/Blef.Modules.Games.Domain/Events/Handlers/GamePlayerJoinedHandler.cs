using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events.Handlers;

internal sealed class GamePlayerJoinedHandler : IDomainEventHandler<GamePlayerJoined>
{
    public Task Handle(GamePlayerJoined @event, CancellationToken cancellation)
    {
        return Task.CompletedTask;
    }
}