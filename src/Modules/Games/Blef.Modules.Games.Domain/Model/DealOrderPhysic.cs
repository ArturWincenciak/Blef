namespace Blef.Modules.Games.Domain.Model;

internal sealed class DealOrderPhysic
{
    private readonly PlayersCount _playersCount;
    private int _shift;

    private DealOrderPhysic(PlayersCount playersCount) =>
        _playersCount = playersCount;

    public static DealOrderPhysic Create(PlayersCount playersCount, DealsCount dealsPlayedCount)
    {
        var orderPhysic = new DealOrderPhysic(playersCount);
        orderPhysic.Move(dealsPlayedCount);
        return orderPhysic;
    }

    public Order ShiftedOrder(Order sequenceIndex)
    {
        if (sequenceIndex.IsGreaterThen(_playersCount))
            throw new ArgumentOutOfRangeException(nameof(sequenceIndex), sequenceIndex,
                message: "Sequence index is greater then players count");

        return sequenceIndex - _shift <= 0
            ? Order.Create(_playersCount + (sequenceIndex - _shift))
            : Order.Create(sequenceIndex - _shift);
    }

    private void Move(DealsCount sequenceShift) =>
        _shift = sequenceShift % _playersCount;
}