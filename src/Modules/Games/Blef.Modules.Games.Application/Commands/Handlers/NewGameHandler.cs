using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Entities;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class NewGameHandler : ICommandHandler<NewGame, NewGame.Result>
{
    private readonly IGamesRepository _games;

    public NewGameHandler(IGamesRepository games) =>
        _games = games;

    public async Task<NewGame.Result> Handle(NewGame command, CancellationToken cancellation)
    {
        var game = Game.Create();
        _games.Add(game);
        var result = new NewGame.Result(game.Id.Id);
        return await Task.FromResult(result);
    }
}