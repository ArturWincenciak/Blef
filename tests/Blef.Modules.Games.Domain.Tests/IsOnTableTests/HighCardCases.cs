using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests;

public static class HighCardCases
{
    public static IEnumerable<object[]> TableWithPokerHand =>
        new List<object[]>
        {
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[] {new Card(FaceCard.Ace, Suit.Clubs)}),
                    new(new[] {new Card(FaceCard.King, Suit.Diamonds)})
                }),
                PokerHandFactory.GivenHighCard(FaceCard.Ace)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[] {new Card(FaceCard.Ace, Suit.Clubs)}),
                    new(new[] {new Card(FaceCard.King, Suit.Hearts)})
                }),
                PokerHandFactory.GivenHighCard(FaceCard.King)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[] {new Card(FaceCard.Queen, Suit.Spades)}),
                    new(new[] {new Card(FaceCard.King, Suit.Hearts)})
                }),
                PokerHandFactory.GivenHighCard(FaceCard.Queen)
            }
        };
}