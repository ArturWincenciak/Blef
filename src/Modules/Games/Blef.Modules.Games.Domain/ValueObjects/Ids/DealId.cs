namespace Blef.Modules.Games.Domain.ValueObjects.Ids;

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