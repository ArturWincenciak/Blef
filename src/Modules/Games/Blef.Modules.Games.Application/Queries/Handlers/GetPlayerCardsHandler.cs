using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetPlayerCardsHandler : IQueryHandler<GetPlayerCards, GetPlayerCards.Result>
{
    private readonly IGameplaysRepository _gameplays;

    public GetPlayerCardsHandler(IGameplaysRepository gameplays) =>
        _gameplays = gameplays;

    public Task<GetPlayerCards.Result> Handle(GetPlayerCards query, CancellationToken cancellation)
    {
        var gameplay = _gameplays.Get(query.Game);
        var hand = gameplay.GetHand(query.Deal, query.Player);
        return Task.FromResult(Map(hand));
    }

    private static GetPlayerCards.Result Map(IEnumerable<Card> hand) =>
        new(hand.Select(card =>
            new GetPlayerCards.Card(FaceCard: card.FaceCard.ToString(), Suit: card.Suit.ToString())));
}