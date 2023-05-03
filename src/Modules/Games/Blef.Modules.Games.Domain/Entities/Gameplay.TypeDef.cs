namespace Blef.Modules.Games.Domain.Entities;

internal sealed partial class Gameplay
{
    internal sealed record DealNumber(int Number);

    internal sealed record GamePlayer(Guid Id, string Nick);

    internal sealed record DealPlayer(Guid PlayerId, List<Card> Hand);

    internal sealed record Deal(
        int Number,
        List<DealPlayer> Players,
        List<Bid> Bids,
        Guid CheckingPlayerId,
        Guid LooserPlayerId);

    internal sealed record Bid(int Order, Guid PlayerId, string PokerHand);

    internal sealed record Card(string FaceCard, string Suit);
}