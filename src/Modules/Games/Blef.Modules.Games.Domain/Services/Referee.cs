using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.Services;

internal sealed class Referee
{
    public PokerHand GetHigherBid(PokerHand firstOne, PokerHand secondOne)
    {
        // todo: ...
        throw new NotImplementedException();
    }

    public bool ContainsPokerHand(IEnumerable<Card> cards, PokerHand pokerHand)
    {
        // todo: ...
        throw new NotImplementedException();
    }
}