using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests;

public static class ThreeOfAKindCases
{
    public static IEnumerable<object[]> TableWithPokerHand =>
        new List<object[]>
        {
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Ten, Suit.Clubs),
                        new Card(FaceCard.Ten, Suit.Diamonds),
                        new Card(FaceCard.Queen, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ten, Suit.Hearts)
                    })
                }),
                PokerHandFactory.GivenThreeOfAKind(FaceCard.Ten)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Hearts)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Clubs)
                    })
                }),
                PokerHandFactory.GivenThreeOfAKind(FaceCard.Queen)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Diamonds),
                        new Card(FaceCard.Ace, Suit.Hearts),
                        new Card(FaceCard.Ace, Suit.Clubs)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Spades)
                    })
                }),
                PokerHandFactory.GivenThreeOfAKind(FaceCard.Ace)
            }
        };
}