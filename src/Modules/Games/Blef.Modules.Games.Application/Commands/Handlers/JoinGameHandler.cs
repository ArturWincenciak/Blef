using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class JoinGameHandler : ICommandHandler<JoinGame, JoinGame.Result>
{
    private readonly IGamesRepository _games;

    public JoinGameHandler(IGamesRepository games) =>
        _games = games;

    public Task<JoinGame.Result> Handle(JoinGame command, CancellationToken cancellation)
    {
        var game = _games.Get(command.GameId);
        var player = game.Join(command.Nick);
        return Task.FromResult(new JoinGame.Result(player.Id, player.Nick));
    }
}