using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class MakeNewGameHandler : ICommandHandler<MakeNewGame, MakeNewGame.Result>
{
    private readonly DeckGenerator _deckGenerator;
    private readonly IGamesRepository _games;

    public MakeNewGameHandler(IGamesRepository games, DeckGenerator deckGenerator)
    {
        _games = games;
        _deckGenerator = deckGenerator;
    }

    public async Task<MakeNewGame.Result> Handle(MakeNewGame command, CancellationToken cancellation)
    {
        var game = Game.Create(_deckGenerator.GetFullDeck());
        _games.Add(game);
        var result = new MakeNewGame.Result(game.Id);
        return await Task.FromResult(result);
    }
}