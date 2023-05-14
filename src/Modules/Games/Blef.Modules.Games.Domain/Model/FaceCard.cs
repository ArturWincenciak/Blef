namespace Blef.Modules.Games.Domain.Model;

internal sealed class FaceCard
{
    public static FaceCard Nine => new(Type.Nine);
    public static FaceCard Ten => new(Type.Ten);
    public static FaceCard Jack => new(Type.Jack);
    public static FaceCard Queen => new(Type.Queen);
    public static FaceCard King => new(Type.King);
    public static FaceCard Ace => new(Type.Ace);

    private readonly Type _faceCard;

    private FaceCard(Type faceCard)
    {
        if (faceCard is Type.None)
            throw new ArgumentException($"{Type.None} type is not allowed");

        _faceCard = faceCard;
    }

    public static FaceCard Create(string faceCard) =>
        new(Parse(faceCard));

    public int GetRank() =>
        (int) _faceCard;

    public static bool operator ==(FaceCard first, FaceCard second) =>
        first.GetRank() == second.GetRank();

    public static bool operator !=(FaceCard first, FaceCard second) =>
        !(first == second);

    public static bool operator >(FaceCard first, FaceCard second) =>
        first.GetRank() > second.GetRank();

    public static bool operator <(FaceCard first, FaceCard second) =>
        !(first > second);

    private bool Equals(FaceCard other) =>
        _faceCard == other._faceCard;

    public override bool Equals(object? obj) =>
        ReferenceEquals(this, obj) || obj is FaceCard other && Equals(other);

    public override int GetHashCode() =>
        (int) _faceCard;

    public override string ToString() =>
        _faceCard.ToString();

    private static Type Parse(string faceCard) =>
        faceCard.ToLower() switch
        {
            "nine" => Type.Nine,
            "ten" => Type.Ten,
            "jack" => Type.Jack,
            "queen" => Type.Queen,
            "king" => Type.King,
            "ace" => Type.Ace,
            _ => throw new ArgumentOutOfRangeException(nameof(faceCard), faceCard,
                $"Unknown value of FaceCard: '{faceCard}'")
        };

    private enum Type
    {
        None,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
}