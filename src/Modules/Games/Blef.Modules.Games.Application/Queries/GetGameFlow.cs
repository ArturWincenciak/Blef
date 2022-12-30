using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetGameFlow(Guid GameId) : IQuery<GetGameFlow.Result>
{
    [UsedImplicitly]
    public sealed record Result(
        Player[] Players,
        GameBid[] Bids,
        Guid CheckingPlayerId,
        Guid LooserPlayerId) : IQueryResult;

    [UsedImplicitly]
    public sealed record GameBid(int Order, Guid PlayerId, string Bid);

    [UsedImplicitly]
    public sealed record Player(Guid Id, string Nick, Card[] Cards);

    [UsedImplicitly]
    public sealed record Card(string FaceCard, string Suit);
}