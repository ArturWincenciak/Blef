using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetGameHandler : IQueryHandler<GetGameFlow, GetGameFlow.Result>
{
    private readonly IGamesRepository _games;

    public GetGameHandler(IGamesRepository games) =>
        _games = games;

    public Task<GetGameFlow.Result> Handle(GetGameFlow query, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    private static GetGameFlow.Card[] HideCards(IEnumerable<Card> cards) =>
        cards.Select(_ => new GetGameFlow.Card(FaceCard: "Hidden", Suit: "Hidden")).ToArray();

    private static GetGameFlow.Card[] MapCards(IEnumerable<Card> cards) =>
        cards.Select(MapCard).ToArray();

    private static GetGameFlow.Card MapCard(Card card) =>
        new(FaceCard: card.FaceCard.ToString(), Suit: card.Suit.ToString());
}