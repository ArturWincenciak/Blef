namespace Blef.Modules.Games.Domain.ValueObjects;

public sealed class DealId
{
    public DealId(GameId gameId, int number)
    {
        if (number < 1)
            throw new ArgumentException("Deal number cannot be less than one");

        GameId = gameId;
        Number = number;
    }

    public GameId GameId { get; }
    public int Number { get; }
}