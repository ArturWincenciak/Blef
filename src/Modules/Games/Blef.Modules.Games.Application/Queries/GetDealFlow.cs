using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetDealFlow(GameId GameId, DealNumber DealNumber) : IQuery<GetDealFlow.Result>
{
    [UsedImplicitly]
    public sealed record Result(
        Player[] Players,
        DealBid[] Bids,
        Guid CheckingPlayerId,
        Guid LooserPlayerId) : IQueryResult;

    [UsedImplicitly]
    public sealed record DealBid(int Order, Guid PlayerId, string Bid);

    [UsedImplicitly]
    public sealed record Player(Guid PlayerId, Card[] Cards);

    [UsedImplicitly]
    public sealed record Card(string FaceCard, string Suit);
}