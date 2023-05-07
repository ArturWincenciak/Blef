namespace Blef.Modules.Games.Domain.Model;

internal sealed class PlayersCount
{
    public int Count { get; }

    public static PlayersCount Create(int count)
    {
        if (count < MIN_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(count), count,
                "TBD"); // todo: exception

        if (count > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(count), count,
                "TBD"); // todo: exception

        return new(count);
    }

    private PlayersCount(int count) =>
        Count = count;
}