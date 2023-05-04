using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Events;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class JoinGameHandler : ICommandHandler<JoinGame, JoinGame.Result>
{
    private readonly IGamesRepository _games;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public JoinGameHandler(IGamesRepository games, IDomainEventDispatcher domainEventDispatcher)
    {
        _games = games;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task<JoinGame.Result> Handle(JoinGame command, CancellationToken cancellation)
    {
        var game = _games.Get(command.GameId);
        var gamePlayerJoined = game.Join(command.Nick);
        await _domainEventDispatcher.Dispatch(gamePlayerJoined, cancellation);
        return new JoinGame.Result(gamePlayerJoined.Player.Id.Id, gamePlayerJoined.Player.Nick.Nick);

        // todo: return in header next possible actions
    }
}