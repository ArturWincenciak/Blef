namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed class CardsAmount
{
    private const int INITIAL_CARDS_AMOUNT = 1;
    public const int MAX_CARDS_AMOUNT = 5;

    public int Value { get; private set; }

    public CardsAmount(int value = INITIAL_CARDS_AMOUNT)
    {
        if (value < INITIAL_CARDS_AMOUNT)
            throw new Exception("TODO: Create exception");

        if (Value > MAX_CARDS_AMOUNT)
            throw new Exception("TODO: Create exception");

        Value = value;
    }

    public void AddOneCard()
    {
        if (Value == MAX_CARDS_AMOUNT)
            throw new Exception("TODO: Create exception");

        Value++;
    }
}