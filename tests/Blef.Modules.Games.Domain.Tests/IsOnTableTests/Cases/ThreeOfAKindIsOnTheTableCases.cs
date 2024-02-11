using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class ThreeOfAKindIsOnTheTableCases : TheoryData<Table, PokerHand>
{
    public ThreeOfAKindIsOnTheTableCases()
    {
        Add(TableCases.GivenTable(new Hand[]
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
            PokerHandFactory.GivenThreeOfAKind(FaceCard.Ten));

        Add(TableCases.GivenTable(new Hand[]
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
            PokerHandFactory.GivenThreeOfAKind(FaceCard.Queen));

        Add(TableCases.GivenTable(new Hand[]
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
            PokerHandFactory.GivenThreeOfAKind(FaceCard.Ace));

        Add(TableCases.GetHighestMaxCardsForFourPlayers(),
            PokerHandFactory.GivenThreeOfAKind(FaceCard.Ten));

        Add(TableCases.GetLowestMaxCardsForFourPlayers(),
            PokerHandFactory.GivenThreeOfAKind(FaceCard.King));
    }
}