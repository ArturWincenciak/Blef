using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetPlayerCardsHandler : IQueryHandler<GetPlayerCards, GetPlayerCards.Result>
{
    private readonly IGameplaysRepository _gameplays;

    public GetPlayerCardsHandler(IGameplaysRepository gameplays) =>
        _gameplays = gameplays;

    public async Task<GetPlayerCards.Result> Handle(GetPlayerCards query, CancellationToken cancellation)
    {
        var gameplay = _gameplays.Get(query.Game.Id);
        var hand = gameplay.GetHand(query.Deal.Number, query.Player.Id);
        return Map(hand);
    }

    private GetPlayerCards.Result Map(IEnumerable<GameplayProjection.Card> hand) =>
        new(hand.Select(card => new GetPlayerCards.Card(card.FaceCard, card.Suit)));
}