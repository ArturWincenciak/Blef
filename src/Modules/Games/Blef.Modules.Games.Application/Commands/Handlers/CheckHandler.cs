using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class CheckHandler : ICommandHandler<Check>
{
    private readonly IGamesRepository _games;

    public CheckHandler(IGamesRepository games) =>
        _games = games;

    public Task Handle(Check command, CancellationToken cancellation)
    {
        // todo
        // var game = _games.Get(command.GameId);
        // var deal = game.GetDeal(command.DealId);
        // game.Check(command.PlayerId);
        return Task.CompletedTask;
    }
}