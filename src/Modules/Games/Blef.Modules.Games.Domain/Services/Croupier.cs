﻿using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Services;

internal sealed class Croupier
{
    private readonly IDeckFactory _deckFactory;

    public Croupier(IDeckFactory deckFactory) =>
        _deckFactory = deckFactory ?? throw new ArgumentNullException(nameof(deckFactory));

    public Deal Deal(DealId dealId, NextDealPlayer[] nextDealPlayers)
    {
        var deck = _deckFactory.Create();
        var players = nextDealPlayers
            .Select(player =>
            {
                var hand = deck.Deal(player.CardsAmount);
                return new DealPlayer(player.PlayerId, hand);
            })
            .ToArray();

        return new (dealId, players);
    }
}