namespace Blef.Modules.Games.Domain.Entities;

public static class CardsGenerator
{
    public static FaceCard[] GetAllFaceCards()
    {
        return new[]
        {
            FaceCard.Nine,
            FaceCard.Ten,
            FaceCard.Jack,
            FaceCard.Queen,
            FaceCard.King,
            FaceCard.Ace
        };
    }

    public static Suit[] GetAllSuites()
    {
        return new[]
        {
            Suit.Clubs,
            Suit.Diamonds,
            Suit.Hearts,
            Suit.Spades
        };
    }
}