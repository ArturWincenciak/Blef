namespace Blef.Modules.Games.Domain.Model;

internal sealed class DealOrderPhysic
{
    private readonly PlayersCount _playersCount;
    private int _shift;

    public static DealOrderPhysic Create(PlayersCount playersCount, DealsCount dealsPlayedCount)
    {
        var orderPhysic = new DealOrderPhysic(playersCount);
        orderPhysic.Move(dealsPlayedCount);
        return orderPhysic;
    }

    public Order ShiftedOrder(Order sequenceIndex)
    {
        if (sequenceIndex - _shift <= 0)
            return Order.Create(_playersCount.Count + (sequenceIndex - _shift));

        return Order.Create(sequenceIndex - _shift);
    }

    private DealOrderPhysic(PlayersCount playersCount) =>
        _playersCount = playersCount;

    private void Move(DealsCount sequenceShift) =>
        _shift = sequenceShift.Count % _playersCount.Count;
}