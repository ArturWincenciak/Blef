using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Services;

internal sealed class Croupier
{
    public Deal Deal(DealId dealId, IEnumerable<NextDealPlayer> nextDealPlayers, Deck deck)
    {
        var players = nextDealPlayers
            .Select(player =>
            {
                var cards = deck.Deal(player.CardsAmount);
                return new DealPlayer(player.PlayerId, cards);
            });

        return new Deal(dealId, players);
    }
}