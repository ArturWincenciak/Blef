namespace Blef.Modules.Games.Domain.ValueObjects.Ids;

public sealed record DealNumber
{
    public int Number { get; }

    public DealNumber(int number)
    {
        if (number < 1)
            throw new ArgumentException("Deal number cannot be less than one");

        Number = number;
    }
}