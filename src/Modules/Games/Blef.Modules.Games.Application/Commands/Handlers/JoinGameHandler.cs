using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class JoinGameHandler : ICommandHandler<JoinGame>
{
    private readonly IGamesRepository _games;

    public JoinGameHandler(IGamesRepository games) =>
        _games = games;

    public Task Handle(JoinGame command, CancellationToken cancellation)
    {
        var game = _games.Get(command.GameId);
        game.Join(command.PlayerId);
        return Task.CompletedTask;
    }
}