namespace Blef.Modules.Games.Domain.Model;

internal sealed class FaceCard
{
    private readonly Type _faceCard;
    public static FaceCard Nine => new(Type.Nine);
    public static FaceCard Ten => new(Type.Ten);
    public static FaceCard Jack => new(Type.Jack);
    public static FaceCard Queen => new(Type.Queen);
    public static FaceCard King => new(Type.King);
    public static FaceCard Ace => new(Type.Ace);

    private FaceCard(Type faceCard) =>
        _faceCard = faceCard;

    public static FaceCard Create(string faceCard) =>
        new(Parse(faceCard));

    public int GetRank() =>
        (int) _faceCard;

    public bool IsBetterThen(FaceCard other) =>
        GetRank() > other.GetRank();

    public override bool Equals(object? obj) =>
        ReferenceEquals(objA: this, obj) || (obj is FaceCard other && Equals(other));

    public override int GetHashCode() =>
        (int) _faceCard;

    public override string ToString() =>
        _faceCard.ToString().ToLower();

    private bool Equals(FaceCard other) =>
        _faceCard == other._faceCard;

    private static Type Parse(string faceCard) =>
        faceCard.ToLower() switch
        {
            "nine" => Type.Nine,
            "ten" => Type.Ten,
            "jack" => Type.Jack,
            "queen" => Type.Queen,
            "king" => Type.King,
            "ace" => Type.Ace,
            _ => throw new ArgumentOutOfRangeException(paramName: nameof(faceCard), faceCard,
                message: $"Unknown value of FaceCard: '{faceCard}'")
        };

    private enum Type
    {
        Nine = 1,
        Ten = 2,
        Jack = 4,
        Queen = 8,
        King = 16,
        Ace = 32
    }
}
