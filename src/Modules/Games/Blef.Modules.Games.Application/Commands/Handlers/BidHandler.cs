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
        var game = await _games.Get(command.GameId);
        var pokerHand = Parse(command.PokerHand);
        var bidPlaced = game.Bid(new Domain.Model.Bid(pokerHand, command.PlayerId));
        await _eventDispatcher.Dispatch(bidPlaced, cancellation);
    }

    private static PokerHand Parse(string bid)
    {
        var parts = bid.Split(":");
        var pokerHandType = parts[0];
        var pokerHandValue = parts[1];

        return pokerHandType.ToLower() switch
        {
            HighCard.TYPE => HighCard.Create(pokerHandValue),
            Pair.TYPE => Pair.Create(pokerHandValue),
            TwoPairs.TYPE => TwoPairs.Create(pokerHandValue),
            LowStraight.TYPE => LowStraight.Create(),
            HighStraight.TYPE => HighStraight.Create(),
            ThreeOfAKind.TYPE => ThreeOfAKind.Create(pokerHandValue),
            FullHouse.TYPE => FullHouse.Create(pokerHandValue),
            Flush.TYPE => Flush.Create(pokerHandValue),
            FourOfAKind.TYPE => FourOfAKind.Create(pokerHandValue),
            StraightFlush.TYPE => StraightFlush.Create(pokerHandValue),
            RoyalFlush.TYPE => RoyalFlush.Create(pokerHandValue),
            _ => throw new Exception($"Unknown type of poker hand: '{pokerHandType}'")
            // todo: validate, exception with problem details, test
        };
    }
}