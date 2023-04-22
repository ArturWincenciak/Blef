namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed record Order
{
    public int Sequence { get; init; }

    public static Order First => new(1);
    public static Order Create(int sequence) => new(sequence);

    public Order Next => new(Sequence + 1);

    private Order(int sequence)
    {
        if (sequence < 1) // todo: exception
            throw new ArgumentOutOfRangeException(nameof(sequence), "TBD");

        if (sequence > MAX_NUMBER_OF_PLAYERS) // todo: exception
            throw new ArgumentOutOfRangeException(nameof(sequence), "TBD");
    }
}