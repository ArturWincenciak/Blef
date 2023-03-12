using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class NewGameHandler : ICommandHandler<NewGame, NewGame.Result>
{
    private readonly DeckGenerator _deckGenerator;
    private readonly IGamesRepository _games;

    public NewGameHandler(IGamesRepository games, DeckGenerator deckGenerator)
    {
        _games = games;
        _deckGenerator = deckGenerator;
    }

    public async Task<NewGame.Result> Handle(NewGame command, CancellationToken cancellation)
    {
        var game = Game.Create(_deckGenerator.GetFullDeck());
        _games.Add(game);
        var result = new NewGame.Result(game.Id);
        return await Task.FromResult(result);
    }
}