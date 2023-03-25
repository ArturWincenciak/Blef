using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetPlayerCardsHandler : IQueryHandler<GetPlayerCards, GetPlayerCards.Result>
{
    private readonly IGamesRepository _games;

    public GetPlayerCardsHandler(IGamesRepository games) =>
        _games = games;

    public Task<GetPlayerCards.Result> Handle(GetPlayerCards query, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    private static GetPlayerCards.Card[] Map(IEnumerable<Card> cards) =>
        cards.Select(Map).ToArray();

    private static GetPlayerCards.Card Map(Card card) =>
        new(FaceCard: card.FaceCard.ToString(), Suit: card.Suit.ToString());
}