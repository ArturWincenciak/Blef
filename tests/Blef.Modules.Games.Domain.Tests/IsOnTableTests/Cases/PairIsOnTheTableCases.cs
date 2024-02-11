using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class PairIsOnTheTableCases : TheoryData<Table, PokerHand>
{
    public PairIsOnTheTableCases()
    {
        Add(TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Nine, Suit.Spades)}),
                new(new[] {new Card(FaceCard.Nine, Suit.Diamonds)})
            }),
            PokerHandFactory.GivenPair(FaceCard.Nine));

        Add(TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Ten, Suit.Diamonds)}),
                new(new[] {new Card(FaceCard.King, Suit.Clubs)}),
                new(new[] {new Card(FaceCard.Ten, Suit.Spades)})
            }),
            PokerHandFactory.GivenPair(FaceCard.Ten));

        Add(TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Ace, Suit.Diamonds)}),
                new(new[] {new Card(FaceCard.Jack, Suit.Spades)}),
                new(new[] {new Card(FaceCard.Jack, Suit.Hearts)}),
                new(new[] {new Card(FaceCard.Ace, Suit.Hearts)})
            }),
            PokerHandFactory.GivenPair(FaceCard.Ace));

        Add(TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Ace, Suit.Diamonds)}),
                new(new[] {new Card(FaceCard.Ace, Suit.Spades)}),
                new(new[] {new Card(FaceCard.Ace, Suit.Hearts)}),
                new(new[] {new Card(FaceCard.Ace, Suit.Clubs)})
            }),
            PokerHandFactory.GivenPair(FaceCard.Ace));

        Add(TableCases.GetHighestMaxCardsForFourPlayers(),
            PokerHandFactory.GivenPair(FaceCard.Ace));

        Add(TableCases.GetLowestMaxCardsForFourPlayers(),
            PokerHandFactory.GivenPair(FaceCard.Nine));
    }
}