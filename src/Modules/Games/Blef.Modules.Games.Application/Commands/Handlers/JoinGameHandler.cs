using Blef.Modules.Games.Application.Exceptions;
using Blef.Modules.Games.Application.Repositories;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Events;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class JoinGameHandler : ICommandHandler<JoinGame, JoinGame.Result>
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly IGamesRepository _games;

    public JoinGameHandler(IGamesRepository games, IDomainEventDispatcher domainEventDispatcher)
    {
        _games = games;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task<JoinGame.Result> Handle(JoinGame command, CancellationToken cancellation)
    {
        var game = await _games.Get(new(command.GameId));
        if (game is null)
            throw new GameNotFoundException(command.GameId);

        var gamePlayerJoined = game.Join(new(command.Nick));
        await _domainEventDispatcher.Dispatch(gamePlayerJoined, cancellation);
        return new(gamePlayerJoined.Player.Id.Id, gamePlayerJoined.Player.Nick.Nick);
    }
}