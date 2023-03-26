namespace Blef.Modules.Games.Domain.ValueObjects.Ids;

public sealed class DealNumber
{
    public DealNumber(int number)
    {
        if (number < 1)
            throw new ArgumentException("Deal number cannot be less than one");

        Number = number;
    }

    public int Number { get; }

    private bool Equals(DealNumber other) =>
        Number == other.Number;

    public override bool Equals(object? obj) =>
        ReferenceEquals(this, obj) || obj is DealNumber other && Equals(other);

    public override int GetHashCode() =>
        Number;
}