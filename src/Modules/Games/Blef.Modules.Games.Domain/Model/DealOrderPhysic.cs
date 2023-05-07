namespace Blef.Modules.Games.Domain.Model;

internal sealed class DealOrderPhysic
{
    private readonly int _playersCount;
    private int shift;

    public DealOrderPhysic(int playersCount) =>
        _playersCount = playersCount;

    public int GetOrder(int baseSequence)
    {
        if (baseSequence - shift <= 0)
            return _playersCount + (baseSequence - shift);

        return baseSequence - shift;
    }

    public void Move(int sequenceShift) =>
        shift = sequenceShift % _playersCount;
}