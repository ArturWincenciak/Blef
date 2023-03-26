using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;
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
        var game = _games.Get(command.GameId);
        var pokerHand = PokerHandParser.Parse(command.PokerHand);
        game.Bid(command.DealNumber, command.PlayerId, pokerHand);
        return Task.CompletedTask;
    }
}