using Blef.Modules.Games.Application.Repositories;
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
        var pokerHand = Parse(command.PokerHand);
        game.Bid(command.DealNumber, new (pokerHand, command.PlayerId));
        return Task.CompletedTask;
    }

    private static PokerHand Parse(string bid)
    {
        var parts = bid.Split(":");
        var pokerHandType = parts[0];

        // todo: implement more Poker Hands
        return pokerHandType.ToLower() switch
        {
            HighCard.Type => HighCard.Deserialize(parts[1]),
            Pair.Type => Pair.Deserialize(parts[1]),
            TwoPairs.Type => TwoPairs.Deserialize(parts[1]),
            LowStraight.Type => LowStraight.Create(),
            HighStraight.Type => HighStraight.Create(),
            _ => throw new Exception($"Unknown type of poker hand: '{pokerHandType}'")
            // todo: validate, domain exception, test
        };
    }




}