using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class StraightFlushIsOnTheTableCases : TheoryData<Table, PokerHand>
{
    public StraightFlushIsOnTheTableCases()
    {
        Add(TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Spades),
                    new Card(FaceCard.Ten, Suit.Spades),
                    new Card(FaceCard.Jack, Suit.Spades),
                    new Card(FaceCard.Queen, Suit.Spades),
                    new Card(FaceCard.King, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Clubs)
                })
            }),
            PokerHandFactory.GivenStraightFlush(Suit.Spades));

        Add(TableCases.GivenTable(new Hand[]
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
                    new Card(FaceCard.King, Suit.Spades)
                })
            }),
            PokerHandFactory.GivenStraightFlush(Suit.Spades));

        Add(TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Diamonds)
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
            PokerHandFactory.GivenStraightFlush(Suit.Diamonds));

        Add(TableCases.GetLowestMaxCardsForFourPlayers(),
            PokerHandFactory.GivenStraightFlush(Suit.Hearts));
    }
}