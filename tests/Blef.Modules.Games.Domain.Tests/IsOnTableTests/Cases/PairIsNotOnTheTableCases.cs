using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class PairIsNotOnTheTableCases : TheoryData<Table, PokerHand>
{
    public PairIsNotOnTheTableCases()
    {
        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Nine, Suit.Spades)}),
                new(new[] {new Card(FaceCard.Nine, Suit.Diamonds)})
            }),
            p2: PokerHandFactory.GivenPair(FaceCard.Ten));

        Add(
            p1: TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Ten, Suit.Diamonds)}),
                new(new[] {new Card(FaceCard.King, Suit.Clubs)}),
                new(new[] {new Card(FaceCard.Ten, Suit.Spades)})
            }),
            p2: PokerHandFactory.GivenPair(FaceCard.King)
        );

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Ace, Suit.Diamonds)}),
                new(new[] {new Card(FaceCard.Jack, Suit.Spades)}),
                new(new[] {new Card(FaceCard.Jack, Suit.Hearts)}),
                new(new[] {new Card(FaceCard.Ace, Suit.Hearts)})
            }),
            p2: PokerHandFactory.GivenPair(FaceCard.King));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Ace, Suit.Diamonds)}),
                new(new[] {new Card(FaceCard.Ace, Suit.Spades)}),
                new(new[] {new Card(FaceCard.Ace, Suit.Hearts)}),
                new(new[] {new Card(FaceCard.Ace, Suit.Clubs)})
            }),
            p2: PokerHandFactory.GivenPair(FaceCard.Nine));

        Add(p1: TableCases.GetLowestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenPair(FaceCard.Ace));

        Add(p1: TableCases.GetHighestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenPair(FaceCard.Nine));
    }
}