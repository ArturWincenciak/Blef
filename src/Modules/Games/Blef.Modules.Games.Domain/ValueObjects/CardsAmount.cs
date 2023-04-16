using System.Runtime.CompilerServices;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed class CardsAmount
{
    private const int INITIAL_CARDS_AMOUNT = 1;
    private const int MAX_CARDS_AMOUNT = 5;

    private int _amount;

    public static CardsAmount Initial => new(INITIAL_CARDS_AMOUNT);
    public static CardsAmount Max => new (MAX_CARDS_AMOUNT);

    private CardsAmount(int amount)
    {
        if (amount < INITIAL_CARDS_AMOUNT)
            throw new AggregateException("Amount cannot be less then one");

        if (_amount > MAX_CARDS_AMOUNT)
            throw new AggregateException("Amount cannot be greater then five");

        _amount = amount;
    }

    public void AddOneCard()
    {
        if (_amount == MAX_CARDS_AMOUNT)
            throw new InvalidOperationException("Amount cannot be greater then five");

        _amount++;
    }

    public static bool operator < (CardsAmount @this, CardsAmount other) =>
        @this._amount < other._amount;

    public static bool operator > (CardsAmount @this, CardsAmount other) =>
        @this._amount > other._amount;

    public static bool operator == (CardsAmount @this, CardsAmount other) =>
        @this._amount == other._amount;

    public static bool operator != (CardsAmount @this, CardsAmount other) =>
        @this._amount != other._amount;

    public static implicit operator int(CardsAmount @this) =>
        @this._amount;
}