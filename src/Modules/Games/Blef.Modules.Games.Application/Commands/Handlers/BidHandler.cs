using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
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
        game.Bid(command.DealNumber, command.PlayerId, pokerHand);
        return Task.CompletedTask;
    }

    private static PokerHand Parse(string bid)
    {
        var parts = bid.Split(":");
        var pokerHandType = parts[0];

        // todo: implement more Poker Hands
        return pokerHandType.ToLower() switch
        {
            HighCard.Type => new HighCard(ParseFaceCard(parts[1])),
            Pair.Type => new Pair(ParseFaceCard(parts[1])),
            TwoPairs.Type => CreateTwoPairs(parts[1]),
            LowStraight.Type => new LowStraight(),
            HighStraight.Type => new HighStraight(),
            _ => throw new Exception($"Unknown type of poker hand: '{pokerHandType}'")
            // todo: validate, domain exception, test
        };
    }

    private static FaceCard ParseFaceCard(string faceCard) =>
        faceCard.ToLower() switch
        {
            "nine" => FaceCard.Nine,
            "ten" => FaceCard.Ten,
            "jack" => FaceCard.Jack,
            "queen" => FaceCard.Queen,
            "king" => FaceCard.King,
            "ace" => FaceCard.Ace,
            _ => throw new Exception($"Unknown value of FaceCard: '{faceCard}'")
        };

    private static TwoPairs CreateTwoPairs(string faceCards)
    {
        var faceCardParts = faceCards.Split(",");
        return new TwoPairs(first: ParseFaceCard(faceCardParts[0]), second: ParseFaceCard(faceCardParts[1]));
    }
}