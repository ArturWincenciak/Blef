using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed record Bid(PokerHand PokerHand, PlayerId Player)
{
    public PokerHand PokerHand { get; } = PokerHand ?? throw new ArgumentNullException(nameof(PokerHand));
    public PlayerId Player { get; } = Player ?? throw new ArgumentNullException(nameof(Player));

    public override string ToString() =>
        PokerHand.Serialize();
}