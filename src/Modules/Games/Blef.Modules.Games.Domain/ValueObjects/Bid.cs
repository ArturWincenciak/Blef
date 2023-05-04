using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed record Bid(PokerHand PokerHand, PlayerId PlayerId)
{
    public PokerHand PokerHand { get; } = PokerHand ?? throw new ArgumentNullException(nameof(PokerHand));
    public PlayerId PlayerId { get; } = PlayerId ?? throw new ArgumentNullException(nameof(PlayerId));

    public override string ToString() =>
        PokerHand.Serialize();
}