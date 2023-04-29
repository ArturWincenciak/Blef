using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Services;

internal sealed class Croupier
{
    private readonly IDeckFactory _deckFactory;

    public Croupier(IDeckFactory deckFactory) =>
        _deckFactory = deckFactory ?? throw new ArgumentNullException(nameof(deckFactory));

    public Deal Deal(DealId dealId, NextDealPlayerSet nextDealPlayerSet)
    {
        var deck = _deckFactory.Create();
        var players = nextDealPlayerSet.Players
            .Select(player =>
            {
                var hand = deck.Deal(player.CardsAmount);
                return new DealPlayer(player.PlayerId, hand);
            })
            .ToArray();

        var moveSequence = new MoveSequence(nextDealPlayerSet.Players
            .Select(player => new Move(player.PlayerId, Order.Create(player.Order))));

        return new (dealId, players, moveSequence);
    }
}