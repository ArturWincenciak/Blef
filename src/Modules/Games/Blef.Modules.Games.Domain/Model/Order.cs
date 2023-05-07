namespace Blef.Modules.Games.Domain.Model;

internal sealed record Order : IComparable<Order>
{
    private readonly int _sequence;

    public static Order First => new(1);
    public static Order Create(int sequence) => new(sequence);

    public Order Next => new(_sequence + 1);

    private Order(int sequence)
    {
        if (sequence < 1)
            throw new ArgumentOutOfRangeException(nameof(sequence), sequence,
                "Sequence cannot be less then one");

        if (sequence > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(sequence), sequence,
                "Sequence cannot be greater then max number of players equals four");

        _sequence = sequence;
    }

    public int CompareTo(Order other) =>
        _sequence.CompareTo(other._sequence);

    public static int operator-(Order order, int value) =>
        order._sequence - value;
}