using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class BidHandler : ICommandHandler<Bid>
{
    private readonly IGamesRepository _games;

    public BidHandler(IGamesRepository games) =>
        _games = games;

    public Task Handle(Bid command, CancellationToken cancellation)
    {
        // todo:
        // var game = _games.Get(command.GameId);
        // var deal = game.GetDeal(command.DealId);
        // deal.Bid(command.PlayerId, command.PokerHand);
        return Task.CompletedTask;
    }
}