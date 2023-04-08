namespace Blef.Modules.Games.Domain.ValueObjects.Cards;


internal sealed class Suit
{
    public static Suit Clubs => new(Type.Clubs);
    public static Suit Diamonds => new(Type.Diamonds);
    public static Suit Hearts => new(Type.Hearts);
    public static Suit Spades => new(Type.Spades);

    private readonly Type _suit;

    private Suit(Type suit)
    {
        if (suit is Type.None) // todo: exception
            throw new ArgumentException("TBD");

        _suit = suit;
    }

    public override string ToString() =>
        _suit.ToString();

    private enum Type
    {
        None,
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
}