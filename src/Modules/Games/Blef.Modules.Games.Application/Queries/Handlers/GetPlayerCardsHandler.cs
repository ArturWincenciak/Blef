using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Queries.Handlers;

internal sealed class GetPlayerCardsHandler : IQueryHandler<GetPlayerCards, GetPlayerCards.Result>
{
    private readonly IGamesRepository _games;

    public GetPlayerCardsHandler(IGamesRepository games) =>
        _games = games;

    public Task<GetPlayerCards.Result> Handle(GetPlayerCards query, CancellationToken cancellation)
    {
        var game = _games.Get(query.GameId);
        var cards = game.GetCards(query.PlayerId);
        var result = new GetPlayerCards.Result(Map(cards));
        return Task.FromResult(result);
    }

    private static GetPlayerCards.Card[] Map(IEnumerable<Card> cards) =>
        cards.Select(Map).ToArray();

    private static GetPlayerCards.Card Map(Card card) =>
        new(FaceCard: card.FaceCard.ToString(), Suit: card.Suit.ToString());
}