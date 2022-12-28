using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetGameFlow(Guid GameId) : IQuery<GetGameFlow.Result>
{
    public sealed record Result(
        Player[] Players,
        GameBid[] Bids,
        Guid CheckingPlayerId,
        Guid LooserPlayerId) : IQueryResult;

    public sealed record GameBid(int Order, Guid PlayerId, string Bid);

    public sealed record Player(Guid Id, string Nick, Card[] Cards);

    public sealed record Card(string FaceCard, string Suit);
}