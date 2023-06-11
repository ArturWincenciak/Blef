using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetPlayerCards(Guid Game, Guid Player, int DealNumber)
    : IQuery<GetPlayerCards.Result>
{
    [UsedImplicitly]
    public sealed record Result(IReadOnlyCollection<Card> Cards) : IQueryResult;

    [UsedImplicitly]
    public sealed record Card(string FaceCard, string Suit);
}
