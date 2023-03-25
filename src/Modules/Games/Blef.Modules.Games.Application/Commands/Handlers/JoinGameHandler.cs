using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class JoinGameHandler : ICommandHandler<JoinGame, JoinGame.Result>
{
    private readonly IGamesRepository _games;

    public JoinGameHandler(IGamesRepository games) =>
        _games = games;

    public Task<JoinGame.Result> Handle(JoinGame command, CancellationToken cancellation)
    {
        var game = _games.Get(command.GameId);
        var player = game.Join(command.Nick);
        var result = new JoinGame.Result(player.Id.Id, player.Nick);
        return Task.FromResult(result);
    }
}