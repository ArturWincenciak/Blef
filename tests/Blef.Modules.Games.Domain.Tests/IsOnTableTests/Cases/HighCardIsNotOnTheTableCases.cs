using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

public static class HighCardIsNotOnTheTableCases
{
    public static IEnumerable<object[]> Cases =>
        new List<object[]>
        {
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[] {new Card(FaceCard.Ace, Suit.Clubs)}),
                    new(new[] {new Card(FaceCard.King, Suit.Diamonds)})
                }),
                PokerHandFactory.GivenHighCard(FaceCard.Jack)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[] {new Card(FaceCard.Ace, Suit.Clubs)}),
                    new(new[] {new Card(FaceCard.King, Suit.Hearts)})
                }),
                PokerHandFactory.GivenHighCard(FaceCard.Queen)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[] {new Card(FaceCard.Queen, Suit.Spades)}),
                    new(new[] {new Card(FaceCard.King, Suit.Hearts)})
                }),
                PokerHandFactory.GivenHighCard(FaceCard.Ace)
            },
            new object[]
            {
                TableCases.GetLowestMaxCardsForFourPlayers(),
                PokerHandFactory.GivenHighCard(FaceCard.Ace)
            },
            new object[]
            {
                TableCases.GetHighestMaxCardsForFourPlayers(),
                PokerHandFactory.GivenHighCard(FaceCard.Nine)
            }
        };
}