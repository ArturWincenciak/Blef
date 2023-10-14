namespace Blef.Modules.Games.Domain.Model;

internal sealed record CardsAmount
{
    private const int INITIAL_CARDS_AMOUNT = 1;
    private const int MAX_CARDS_AMOUNT = 5;

    private readonly int _amount;

    public static CardsAmount Initial => new(INITIAL_CARDS_AMOUNT);
    public static CardsAmount Max => new(MAX_CARDS_AMOUNT);

    private CardsAmount(int amount)
    {
        if (amount < INITIAL_CARDS_AMOUNT)
            throw new ArgumentOutOfRangeException(paramName: nameof(amount), amount,
                message: "Amount cannot be less then one");

        if (_amount > MAX_CARDS_AMOUNT)
            throw new ArgumentOutOfRangeException(paramName: nameof(amount), amount,
                message: "Amount cannot be greater then five");

        _amount = amount;
    }

    public CardsAmount AddOneCard()
    {
        if (_amount == MAX_CARDS_AMOUNT)
            throw new InvalidOperationException("Amount cannot be greater then five");

        return new CardsAmount(_amount + 1);
    }

    public bool IsLowerThen(CardsAmount other) =>
        _amount < other._amount;

    public static implicit operator int(CardsAmount @this) =>
        @this._amount;
}