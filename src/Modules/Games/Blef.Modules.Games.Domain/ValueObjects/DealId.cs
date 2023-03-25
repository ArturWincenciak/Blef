namespace Blef.Modules.Games.Domain.ValueObjects;

public sealed class DealId
{
    public DealId(GameId gameId, DealNumber number)
    {
        GameId = gameId;
        Number = number;
    }

    public GameId GameId { get; }
    public DealNumber Number { get; }
}