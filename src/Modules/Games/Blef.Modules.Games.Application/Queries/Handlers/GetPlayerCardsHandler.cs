using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetPlayerCardsHandler : IQueryHandler<GetPlayerCards, GetPlayerCards.Result>
{
    private readonly IGameplaysRepository _gameplays;

    public GetPlayerCardsHandler(IGameplaysRepository gameplays) =>
        _gameplays = gameplays;

    public async Task<GetPlayerCards.Result> Handle(GetPlayerCards query, CancellationToken cancellation)
    {
        var gameplay = _gameplays.Get(query.Game);
        var hand = gameplay.GetHand(query.Deal, query.Player);
        return Map(hand);
    }

    private GetPlayerCards.Result Map(IEnumerable<Card> hand) =>
        new(hand.Select(card => new GetPlayerCards.Card(card.FaceCard.ToString(), card.Suit.ToString())));
}