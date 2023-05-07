namespace Blef.Modules.Games.Domain.Model;

internal sealed class DealsCount
{
    private readonly int _count;

    public static DealsCount Create(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), count,
                "TBD"); // todo: exception

        return new DealsCount(count);
    }

    private DealsCount(int count) =>
        _count = count;

    public static int operator %(DealsCount dealsCount, PlayersCount playersCount) =>
        dealsCount._count % playersCount;

}