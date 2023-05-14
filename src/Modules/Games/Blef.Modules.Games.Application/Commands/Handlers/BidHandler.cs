using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Events;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class BidHandler : ICommandHandler<Bid>
{
    private readonly IDomainEventDispatcher _eventDispatcher;
    private readonly IGamesRepository _games;

    public BidHandler(IGamesRepository games, IDomainEventDispatcher eventDispatcher)
    {
        _games = games;
        _eventDispatcher = eventDispatcher;
    }

    public async Task Handle(Bid command, CancellationToken cancellation)
    {
        var game = _games.Get(command.GameId);
        var pokerHand = Parse(command.PokerHand);
        var bidPlaced = game.Bid(new Domain.Model.Bid(pokerHand, command.PlayerId));
        await _eventDispatcher.Dispatch(bidPlaced, cancellation);
    }

    private static PokerHand Parse(string bid)
    {
        var parts = bid.Split(":");
        var pokerHandType = parts[0];

        // todo: implement more Poker Hands
        return pokerHandType.ToLower() switch
        {
            HighCard.Type => HighCard.Create(parts[1]),
            Pair.Type => Pair.Create(parts[1]),
            TwoPairs.Type => TwoPairs.Create(parts[1]),
            LowStraight.Type => LowStraight.Create(),
            HighStraight.Type => HighStraight.Create(),
            _ => throw new Exception($"Unknown type of poker hand: '{pokerHandType}'")
            // todo: validate, exception with problem details, test
        };
    }
}