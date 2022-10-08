using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class BidHandler : ICommandHandler<Bid>
{
    private readonly Domain.Games _games;

    public BidHandler(Domain.Games games) =>
        _games = games;

    public Task Handle(Bid command, CancellationToken cancellation)
    {
        var game = _games.GetExistingGame(command.GameId);
        game.Bid(command.PlayerId, command.PokerHand);
        return Task.CompletedTask;
    }
}