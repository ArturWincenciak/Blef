using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetPlayerCards(GameId GameId, PlayerId PlayerId, DealNumber DealNumber)
    : IQuery<GetPlayerCards.Result>
{
    [UsedImplicitly]
    public sealed record Result(Card[] Cards) : IQueryResult;

    [UsedImplicitly]
    public sealed record Card(string FaceCard, string Suit);
}