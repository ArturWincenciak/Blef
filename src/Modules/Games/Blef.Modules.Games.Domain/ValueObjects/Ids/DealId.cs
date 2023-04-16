namespace Blef.Modules.Games.Domain.ValueObjects.Ids;

internal sealed record DealId(GameId GameId, DealNumber Number)
{
    public GameId GameId { get; } =
        GameId ?? throw new ArgumentNullException(nameof(GameId));

    public DealNumber Number { get; } =
        Number ?? throw new ArgumentNullException(nameof(Number));
}