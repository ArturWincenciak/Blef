using Blef.Modules.Games.Application.Exceptions;
using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Events;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class BidHandler : ICommandHandler<Bid, Bid.Result>
{
    private readonly IDomainEventDispatcher _eventDispatcher;
    private readonly IGamesRepository _games;

    public BidHandler(IGamesRepository games, IDomainEventDispatcher eventDispatcher)
    {
        _games = games;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<Bid.Result> Handle(Bid command, CancellationToken cancellation)
    {
        var game = await _games.Get(new(command.GameId));
        if (game is null)
            throw new GameNotFoundException(command.GameId);

        var pokerHand = PokerHand.Map(command.PokerHand);
        var bidPlaced = game.Bid(new(pokerHand, Player: new(command.PlayerId)));
        await _eventDispatcher.Dispatch(bidPlaced, cancellation);
        return new(bidPlaced.Deal.Number);
    }
}