using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetPlayerCards(GameId Game, PlayerId Player, DealNumber Deal)
    : IQuery<GetPlayerCards.Result>
{
    [UsedImplicitly]
    public sealed record Result(IEnumerable<Card> Cards) : IQueryResult;

    [UsedImplicitly]
    public sealed record Card(string FaceCard, string Suit);
}