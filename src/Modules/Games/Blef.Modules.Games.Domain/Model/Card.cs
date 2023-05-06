namespace Blef.Modules.Games.Domain.Model;

internal sealed record Card(FaceCard FaceCard, Suit Suit)
{
    public FaceCard FaceCard { get; } = FaceCard ?? throw new ArgumentNullException(nameof(FaceCard));
    public Suit Suit { get; } = Suit ?? throw new ArgumentNullException(nameof(Suit));
}