namespace Blef.Modules.Games.Domain.Model;

internal sealed class DealsCount
{
    public int Count { get; }

    public static DealsCount Create(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), count,
                "TBD"); // todo: exception

        return new DealsCount(count);
    }

    private DealsCount(int count) =>
        Count = count;
}