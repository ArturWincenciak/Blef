namespace Blef.Modules.Games.Domain.Entities;

internal sealed partial class GameplayProjection
{
    internal sealed record GamePlayer(Guid Id, string Nick);

    internal sealed record DealPlayer(Guid PlayerId, List<Card> Hand);

    internal sealed record Deal(
        int Number,
        List<DealPlayer> Players,
        List<Bid> Bids,
        Guid CheckingPlayerId = default,
        Guid LooserPlayerId = default);

    internal sealed record Bid(int Order, Guid PlayerId, string PokerHand);

    internal sealed record Card(string FaceCard, string Suit);

    internal sealed record GameProjection(IEnumerable<GamePlayer> GamePlayers, IEnumerable<int> DealNumbers);

    public sealed record DealProjection(
        IEnumerable<DealPlayer> Players,
        IEnumerable<Bid> Bids,
        Guid CheckingPlayerId,
        Guid LooserPlayerId);
}