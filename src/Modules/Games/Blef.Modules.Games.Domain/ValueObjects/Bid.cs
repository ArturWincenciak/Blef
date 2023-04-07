using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed record Bid(PokerHand PokerHand, PlayerId Player);