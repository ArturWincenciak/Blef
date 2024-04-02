namespace Blef.Modules.Games.Domain.Model;

internal sealed class DealsCount
{
    private readonly int _count;

    private DealsCount(int count) =>
        _count = count;

    public static DealsCount Create(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(paramName: nameof(count), count,
                message: "Count cannot be negative");

        return new(count);
    }

    public static int operator %(DealsCount dealsCount, PlayersCount playersCount) =>
        dealsCount._count % playersCount;
}