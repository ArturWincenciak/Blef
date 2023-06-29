using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Model;
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
        var game = await _games.Get(new GameId(command.GameId));
        var pokerHand = Parse(command.PokerHand);
        var bidPlaced = game.Bid(new Domain.Model.Bid(pokerHand, Player: new PlayerId(command.PlayerId)));
        await _eventDispatcher.Dispatch(bidPlaced, cancellation);
        return new Bid.Result(bidPlaced.Deal.Number);
    }

    private static PokerHand Parse(string bid)
    {
        var parts = bid.Split(":");
        var pokerHandType = parts[0];
        string PokerHandValue() => parts[1];

        return pokerHandType.ToLower() switch
        {
            HighCard.TYPE => HighCard.Create(PokerHandValue()),
            Pair.TYPE => Pair.Create(PokerHandValue()),
            TwoPairs.TYPE => TwoPairs.Create(PokerHandValue()),
            LowStraight.TYPE => LowStraight.Create(),
            HighStraight.TYPE => HighStraight.Create(),
            ThreeOfAKind.TYPE => ThreeOfAKind.Create(PokerHandValue()),
            FullHouse.TYPE => FullHouse.Create(PokerHandValue()),
            Flush.TYPE => Flush.Create(PokerHandValue()),
            FourOfAKind.TYPE => FourOfAKind.Create(PokerHandValue()),
            StraightFlush.TYPE => StraightFlush.Create(PokerHandValue()),
            RoyalFlush.TYPE => RoyalFlush.Create(PokerHandValue()),
            _ => throw new Exception($"Unknown type of poker hand: '{pokerHandType}'")
        };
    }
}
