namespace Blef.Modules.Games.Domain.ValueObjects.Cards;

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
        if (faceCard is Type.None) // todo: exception
            throw new ArgumentException("TBD");

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

    private static Type Parse(string faceCard) =>
        faceCard.ToLower() switch
        {
            "nine" => Type.Nine,
            "ten" => Type.Ten,
            "jack" => Type.Jack,
            "queen" => Type.Queen,
            "king" => Type.King,
            "ace" => Type.Ace,
            _ => throw new Exception($"Unknown value of FaceCard: '{faceCard}'")
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