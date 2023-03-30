﻿using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetPlayerCardsHandler : IQueryHandler<GetPlayerCards, GetPlayerCards.Result>
{
    private readonly IGamesRepository _games;

    public GetPlayerCardsHandler(IGamesRepository games) =>
        _games = games;

    public async Task<GetPlayerCards.Result> Handle(GetPlayerCards query, CancellationToken cancellation)
    {
        var game = _games.Get(query.GameId);
        var cards = game.GetCards(query.PlayerId, query.DealNumber);
        var result = Map(cards);
        return new GetPlayerCards.Result(result);
    }

    private static GetPlayerCards.Card[] Map(IEnumerable<Card> cards) =>
        cards.Select(Map).ToArray();

    private static GetPlayerCards.Card Map(Card card) =>
        new(FaceCard: card.FaceCard.ToString(), Suit: card.Suit.ToString());
}