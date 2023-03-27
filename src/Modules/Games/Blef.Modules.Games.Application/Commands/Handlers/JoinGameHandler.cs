using Blef.Modules.Games.Application.Repositories;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class JoinGameHandler : ICommandHandler<JoinGame, JoinGame.Result>
{
    private readonly IGamesRepository _games;

    public JoinGameHandler(IGamesRepository games) =>
        _games = games;

    public async Task<JoinGame.Result> Handle(JoinGame command, CancellationToken cancellation)
    {
        var game = _games.Get(command.GameId);
        var player = game.Join(new (command.Nick));
        return new JoinGame.Result(player.PlayerId.Id, player.Nick.Nick);
    }
}