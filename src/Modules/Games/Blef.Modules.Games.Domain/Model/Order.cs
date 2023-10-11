namespace Blef.Modules.Games.Domain.Model;

internal sealed record Order : IComparable<Order>
{
    public static Order First => new(1);

    public Order Next => new(Int + 1);

    public int Int { get; }

    private Order(int sequence) =>
        Int = sequence switch
        {
            < 1 => throw new ArgumentOutOfRangeException(paramName: nameof(sequence), sequence,
                message: "Sequence cannot be less then one"),
            > MAX_NUMBER_OF_PLAYERS => throw new ArgumentOutOfRangeException(paramName: nameof(sequence), sequence,
                message: $"Sequence cannot be greater then max number of players equals {MAX_NUMBER_OF_PLAYERS}"),
            _ => sequence
        };

    public int CompareTo(Order? other) =>
        Int.CompareTo(other!.Int);

    public static Order Create(int sequence) => new(sequence);

    public static int operator -(Order order, int value) =>
        order.Int - value;

    public override string ToString() =>
        Int.ToString();
}