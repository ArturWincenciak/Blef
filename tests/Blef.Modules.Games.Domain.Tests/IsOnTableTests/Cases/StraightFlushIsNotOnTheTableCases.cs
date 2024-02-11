using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class StraightFlushIsNotOnTheTableCases : TheoryData<Table, PokerHand>
{
    public StraightFlushIsNotOnTheTableCases()
    {
        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Spades),
                    new Card(FaceCard.Ten, Suit.Spades),
                    new Card(FaceCard.Jack, Suit.Spades),
                    new Card(FaceCard.Queen, Suit.Spades),
                    new Card(FaceCard.King, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Clubs)
                })
            }),
            p2: PokerHandFactory.GivenStraightFlush(Suit.Spades));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Spades),
                    new Card(FaceCard.Ten, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Jack, Suit.Spades),
                    new Card(FaceCard.Queen, Suit.Spades),
                    new Card(FaceCard.Ace, Suit.Spades)
                })
            }),
            p2: PokerHandFactory.GivenStraightFlush(Suit.Spades));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Hearts)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ten, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.Jack, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.Queen, Suit.Diamonds),
                    new Card(FaceCard.King, Suit.Diamonds)
                })
            }),
            p2: PokerHandFactory.GivenStraightFlush(Suit.Diamonds));

        Add(p1: TableCases.GetHighestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenStraightFlush(Suit.Hearts));
    }
}