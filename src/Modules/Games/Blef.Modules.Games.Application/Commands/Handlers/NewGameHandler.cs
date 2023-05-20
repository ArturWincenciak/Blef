using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Services;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class NewGameHandler : ICommandHandler<NewGame, NewGame.Result>
{
    private readonly GameFactory _gameFactory;
    private readonly IGameplaysRepository _gameplays;
    private readonly IGamesRepository _games;

    public NewGameHandler(IGamesRepository games, GameFactory gameFactory, IGameplaysRepository gameplays)
    {
        _games = games;
        _gameFactory = gameFactory;
        _gameplays = gameplays;
    }

    public async Task<NewGame.Result> Handle(NewGame command, CancellationToken cancellation)
    {
        var game = _gameFactory.Create();
        await _games.Add(game);
        await _gameplays.Add(new Gameplay(game.Id));
        return new NewGame.Result(game.Id.Id);
    }
}