namespace Blef.Modules.Games.Domain.ValueObjects.Cards;

internal record Card
{
    public FaceCard FaceCard { get; }
    public Suit Suit { get; }

    public Card(FaceCard faceCard, Suit suit)
    {
        if (faceCard == FaceCard.None) // todo: exception
            throw new Exception("TBD");

        if (suit == Suit.None) // todo: exception
            throw new Exception("TBD");

        FaceCard = faceCard;
        Suit = suit;
    }
}