namespace Blef.Modules.Games.Domain.Model;

internal sealed class PlayersCount
{
    private readonly int _count;

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
        _count = count;

    public static int operator +(PlayersCount dealsCount, int value) =>
        dealsCount._count + value;

    public static implicit operator int(PlayersCount playersCount) =>
        playersCount._count;
}