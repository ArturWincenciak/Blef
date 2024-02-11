using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class TwoPairsAreOnTheTableCases : TheoryData<Table, PokerHand>
{
    public TwoPairsAreOnTheTableCases()
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
                    new Card(FaceCard.Nine, Suit.Clubs),
                    new Card(FaceCard.Ace, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.Jack, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Hearts)
                })
            }),
            p2: PokerHandFactory.GivenTwoPairsBid(FaceCard.Nine, FaceCard.Ace));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Diamonds),
                    new Card(FaceCard.Ten, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.King, Suit.Clubs),
                    new Card(FaceCard.King, Suit.Hearts)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ten, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Spades)
                })
            }),
            p2: PokerHandFactory.GivenTwoPairsBid(FaceCard.King, FaceCard.Ten));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Queen, Suit.Diamonds)}),
                new(new[] {new Card(FaceCard.Jack, Suit.Spades)}),
                new(new[] {new Card(FaceCard.Jack, Suit.Hearts)}),
                new(new[] {new Card(FaceCard.Queen, Suit.Hearts)})
            }),
            p2: PokerHandFactory.GivenTwoPairsBid(FaceCard.Jack, FaceCard.Queen));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.King, Suit.Diamonds),
                    new Card(FaceCard.King, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.King, Suit.Clubs),
                    new Card(FaceCard.King, Suit.Hearts)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ten, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ten, Suit.Hearts)
                })
            }),
            p2: PokerHandFactory.GivenTwoPairsBid(FaceCard.King, FaceCard.Ten));

        Add(p1: TableCases.GetHighestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenTwoPairsBid(FaceCard.Ace, FaceCard.King));

        Add(p1: TableCases.GetLowestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenTwoPairsBid(FaceCard.Nine, FaceCard.Ten));
    }
}