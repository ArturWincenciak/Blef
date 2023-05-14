namespace Blef.Modules.Games.Domain.Model;

internal sealed class PlayersCount
{
    private readonly int _count;

    private PlayersCount(int count) =>
        _count = count;

    public static PlayersCount Create(int count)
    {
        if (count < MIN_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(paramName: nameof(count), count,
                message: $"The player count should be at least {MIN_NUMBER_OF_PLAYERS}");

        if (count > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(paramName: nameof(count), count,
                message: $"The player count cannot exceed {MAX_NUMBER_OF_PLAYERS}");

        return new PlayersCount(count);
    }

    public static int operator +(PlayersCount dealsCount, int value) =>
        dealsCount._count + value;

    public static implicit operator int(PlayersCount playersCount) =>
        playersCount._count;
}