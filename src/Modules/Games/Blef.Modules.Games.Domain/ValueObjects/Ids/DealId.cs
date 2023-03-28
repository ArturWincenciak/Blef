namespace Blef.Modules.Games.Domain.ValueObjects.Ids;

public sealed record DealId(GameId GameId, DealNumber Number)
{
    public GameId GameId { get; init; } =
        GameId ?? throw new ArgumentNullException(nameof(GameId));

    public DealNumber Number { get; init; } =
        Number ?? throw new ArgumentNullException(nameof(Number));
}