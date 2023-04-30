namespace Blef.Modules.Games.Domain.ValueObjects;

// todo: test
internal sealed record Order
{
    private readonly int _sequence;

    public static Order First => new(1);
    public static Order Create(int sequence) => new(sequence);

    public Order Next => new(_sequence + 1);

    private Order(int sequence)
    {
        if (sequence < 1)
            throw new ArgumentOutOfRangeException(nameof(sequence),
                "Sequence cannot be less then one");

        if (sequence > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(sequence),
                "Sequence cannot be greater then max number of players equals four");

        _sequence = sequence;
    }
}