namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed class CardsAmount
{
    private const int INITIAL_CARDS_AMOUNT = 1;
    public const int MAX_CARDS_AMOUNT = 5;

    public int Amount { get; private set; }

    public static CardsAmount Initial => new(amount: 1);

    private CardsAmount(int amount)
    {
        if (amount < INITIAL_CARDS_AMOUNT) // todo: exception
            throw new Exception("TBD");

        if (Amount > MAX_CARDS_AMOUNT) // todo: exception
            throw new Exception("TBD");

        Amount = amount;
    }

    public void AddOneCard()
    {
        if (Amount == MAX_CARDS_AMOUNT) // todo: exception
            throw new Exception("TBD");

        Amount++;
    }
}