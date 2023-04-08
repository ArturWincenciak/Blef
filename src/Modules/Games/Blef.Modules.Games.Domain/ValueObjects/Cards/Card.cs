namespace Blef.Modules.Games.Domain.ValueObjects.Cards;

internal sealed record Card
{
    public FaceCard FaceCard { get; }
    public Suit Suit { get; }

    public Card(FaceCard faceCard, Suit suit)
    {
        if (suit == Suit.None) // todo: exception
            throw new Exception("TBD");

        FaceCard = faceCard;
        Suit = suit;
    }
}