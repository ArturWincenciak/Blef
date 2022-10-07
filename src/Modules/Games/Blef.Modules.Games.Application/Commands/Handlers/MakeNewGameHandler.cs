using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class MakeNewGameHandler : ICommandHandler<MakeNewGame, MakeNewGame.Result>
{
    private readonly Domain.Games _games;

    public MakeNewGameHandler(Domain.Games games) =>
        _games = games;

    public async Task<MakeNewGame.Result> Handle(MakeNewGame command, CancellationToken cancellation)
    {
        var gameId = _games.MakeNewGame();
        var result = new MakeNewGame.Result(gameId);
        return await Task.FromResult(result);
    }
}