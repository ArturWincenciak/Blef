namespace Blef.Modules.Games.Domain.Model;

internal sealed class DealOrderPhysic
{
    private readonly int _playersCount;
    private int shift;

    public static DealOrderPhysic Create(int playersCount, int dealsPlayedCount)
    {
        if (playersCount < MIN_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(playersCount), playersCount,
                "TBD"); // todo: exception

        if (playersCount > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(playersCount), playersCount,
                "TBD"); // todo: exception

        if (dealsPlayedCount < 0)
            throw new ArgumentOutOfRangeException(nameof(dealsPlayedCount),
                "TOB"); // todo: exception

        var orderPhysic = new DealOrderPhysic(playersCount);
        orderPhysic.Move(dealsPlayedCount);
        return orderPhysic;
    }

    public Order Order(Order sequenceIndex)
    {
        if (sequenceIndex - shift <= 0)
            return Model.Order.Create(_playersCount + (sequenceIndex - shift));

        return Model.Order.Create(sequenceIndex - shift);
    }

    private DealOrderPhysic(int playersCount) =>
        _playersCount = playersCount;

    private void Move(int sequenceShift) =>
        shift = sequenceShift % _playersCount;
}