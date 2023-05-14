namespace Blef.Modules.Games.Domain.Model;

internal sealed class Suit
{
    private readonly Type _suit;
    public static Suit Clubs => new(Type.Clubs);
    public static Suit Diamonds => new(Type.Diamonds);
    public static Suit Hearts => new(Type.Hearts);
    public static Suit Spades => new(Type.Spades);

    private Suit(Type suit)
    {
        if (suit is Type.None)
            throw new ArgumentException($"{Type.None} type is not allowed");

        _suit = suit;
    }

    private bool Equals(Suit other) =>
        _suit == other._suit;

    public override bool Equals(object? obj) =>
        ReferenceEquals(objA: this, obj) || (obj is Suit other && Equals(other));

    public override int GetHashCode() =>
        (int) _suit;

    public override string ToString() =>
        _suit.ToString();

    private enum Type
    {
        None = 0,
        Clubs = 1,
        Diamonds = 2,
        Hearts = 4,
        Spades = 8
    }
}