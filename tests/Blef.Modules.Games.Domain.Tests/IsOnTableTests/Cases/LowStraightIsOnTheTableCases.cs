using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class LowStraightIsOnTheTableCases : TheoryData<Table, PokerHand>
{
    public LowStraightIsOnTheTableCases()
    {
        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Spades),
                    new Card(FaceCard.Ten, Suit.Clubs)
                }),
                new(new[]
                {
                    new Card(FaceCard.Jack, Suit.Clubs)
                }),
                new(new[]
                {
                    new Card(FaceCard.Queen, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.King, Suit.Diamonds)
                })
            }),
            p2: LowStraight.Create());

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Ten, Suit.Diamonds),
                    new Card(FaceCard.Nine, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.Queen, Suit.Hearts),
                    new Card(FaceCard.Ace, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Jack, Suit.Clubs)
                }),
                new(new[]
                {
                    new Card(FaceCard.King, Suit.Spades)
                })
            }),
            p2: LowStraight.Create());

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Hearts),
                    new Card(FaceCard.Nine, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ten, Suit.Spades),
                    new Card(FaceCard.Jack, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Jack, Suit.Diamonds),
                    new Card(FaceCard.Queen, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.King, Suit.Spades)
                })
            }),
            p2: LowStraight.Create());

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Spades),
                    new Card(FaceCard.Nine, Suit.Hearts)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ten, Suit.Spades),
                    new Card(FaceCard.Ten, Suit.Hearts)
                }),
                new(new[]
                {
                    new Card(FaceCard.Jack, Suit.Spades),
                    new Card(FaceCard.Jack, Suit.Hearts),
                    new Card(FaceCard.King, Suit.Hearts)
                }),
                new(new[]
                {
                    new Card(FaceCard.Queen, Suit.Spades),
                    new Card(FaceCard.Queen, Suit.Hearts),
                    new Card(FaceCard.King, Suit.Spades)
                })
            }),
            p2: LowStraight.Create());

        Add(p1: TableCases.GetLowestMaxCardsForFourPlayers(),
            p2: LowStraight.Create());
    }
}