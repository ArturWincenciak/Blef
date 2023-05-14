namespace Blef.Modules.Games.Domain.Model;

// todo: test this class
internal sealed class PlayersCount
{
    private readonly int _count;

    public static PlayersCount Create(int count)
    {
        if (count < MIN_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(count), count,
                $"The player count should be at least {MIN_NUMBER_OF_PLAYERS}");

        if (count > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(count), count,
                $"The player count cannot exceed {MAX_NUMBER_OF_PLAYERS}");

        return new(count);
    }

    private PlayersCount(int count) =>
        _count = count;

    public static int operator +(PlayersCount dealsCount, int value) =>
        dealsCount._count + value;

    public static implicit operator int(PlayersCount playersCount) =>
        playersCount._count;
}