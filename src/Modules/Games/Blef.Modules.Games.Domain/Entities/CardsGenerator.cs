namespace Blef.Modules.Games.Domain.Entities;

public static class CardsGenerator
{
    public static IEnumerable<FaceCard> GetAllFaceCards() =>
        new[]
        {
            FaceCard.Nine,
            FaceCard.Ten,
            FaceCard.Jack,
            FaceCard.Queen,
            FaceCard.King,
            FaceCard.Ace
        };

    public static IEnumerable<Suit> GetAllSuites() =>
        new[]
        {
            Suit.Clubs,
            Suit.Diamonds,
            Suit.Hearts,
            Suit.Spades
        };
}