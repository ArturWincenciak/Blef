namespace Blef.Modules.Games.Domain.Model;

internal sealed record Order : IComparable<Order>
{
    private readonly int _sequence;

    public static Order First => new(1);

    public Order Next => new(_sequence + 1);

    private Order(int sequence) =>
        _sequence = sequence switch
        {
            < 1 => throw new ArgumentOutOfRangeException(paramName: nameof(sequence), sequence,
                message: "Sequence cannot be less then one"),
            > MAX_NUMBER_OF_PLAYERS => throw new ArgumentOutOfRangeException(paramName: nameof(sequence), sequence,
                message: $"Sequence cannot be greater then max number of players equals {MAX_NUMBER_OF_PLAYERS}"),
            _ => sequence
        };

    public int CompareTo(Order? other) =>
        _sequence.CompareTo(other!._sequence);

    public static Order Create(int sequence) => new(sequence);

    public static int operator -(Order order, int value) =>
        order._sequence - value;

    public int Int => _sequence;

    public override string ToString() =>
        _sequence.ToString();
}