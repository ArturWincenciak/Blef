using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Services;

internal sealed class Croupier
{
    private readonly IDeckFactory _deckFactory;

    public Croupier(IDeckFactory deckFactory) =>
        _deckFactory = deckFactory ?? throw new ArgumentNullException(nameof(deckFactory));

    public DealSet Deal(NextDealPlayersSet nextDealPlayersSet)
    {
        var deck = _deckFactory.Create();
        var players = nextDealPlayersSet.Players
            .Select(nextDealPlayer =>
            {
                var hand = deck.Deal(nextDealPlayer.CardsAmount);
                return new DealPlayer(nextDealPlayer.Player, hand);
            })
            .ToArray();

        var moveSequence = new MoveSequence(nextDealPlayersSet.Players
            .Select(player => new Move(player.Player, Order.Create(player.Order))));

        return new(new(players), moveSequence);
    }
}