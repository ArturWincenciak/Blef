using Blef.Modules.Games.Domain;
using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetPlayerCards(
    Guid GameId, Guid PlayerId) : IQuery<GetPlayerCards.Result>
{
    public sealed record Result(
        Card[] Cards) : IQueryResult;
}