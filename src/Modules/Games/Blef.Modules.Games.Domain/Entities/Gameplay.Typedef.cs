namespace Blef.Modules.Games.Domain.Entities;

internal sealed partial class Gameplay
{
    internal sealed record GamePlayer(
        Guid Id,
        string Nick);

    internal sealed record DealNumber(
        int Number);

    internal sealed record Deal(
        int Number,
        List<Deal.DealPlayer> Players,
        List<Deal.Bid> Bids,
        Guid CheckingPlayerId,
        Guid LooserPlayerId)
    {
        internal sealed record DealPlayer(
            Guid PlayerId,
            List<DealPlayer.Card> Hand)
        {
            internal sealed record Card(
                string FaceCard,
                string Suit);
        }

        internal sealed record Bid(
            int Order,
            Guid PlayerId,
            string PokerHand);
    }
}