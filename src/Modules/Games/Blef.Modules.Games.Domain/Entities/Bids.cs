namespace Blef.Modules.Games.Domain.Entities;

public static class Bids
{
    public static int Compare(string firstBid, string secondBid)
    {
        // TODO: implement more Poker Hands
        return PokerHand.Parse(secondBid).FaceCard.CompareTo(PokerHand.Parse(firstBid).FaceCard);
    }
}