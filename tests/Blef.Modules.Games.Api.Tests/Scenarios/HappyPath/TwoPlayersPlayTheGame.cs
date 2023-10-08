using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

[UsesVerify]
public class TwoPlayersPlayTheGame
{
    [Fact]
    public async Task Scenario()
    {
        var results = await new TestBuilder()

            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .NewDeal()
            .GetGameFlow()
            .GetDealFlow(new DealNumber(1))

            .GetCards(WhichPlayer.Knuth, new DealNumber(1))
            .GetCards(WhichPlayer.Graham, new DealNumber(1))
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Nine, description: "Knuth bids high card Nine in deal 1")
            .Check(WhichPlayer.Graham,
                description: "Graham checks in deal 1 and Knuth get lost (Knuth has 2 cards in next deal)")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(1))

            .GetCards(WhichPlayer.Knuth, new DealNumber(2))
            .GetCards(WhichPlayer.Graham, new DealNumber(2))
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "Graham bids high card Ace in deal 2")
            .Check(WhichPlayer.Knuth,
                description: "Knuth checks in deal 2 and get lost and has 3 cards in next deal")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(2))

            .GetCards(WhichPlayer.Knuth, new DealNumber(3))
            .GetCards(WhichPlayer.Graham, new DealNumber(3))
            .BidPair(WhichPlayer.Knuth, FaceCard.King, description: "Knuth bids pair King in deal 3")
            .BidPair(WhichPlayer.Graham, FaceCard.Ace, description: "Graham bids pair Ace in deal 3")
            .Check(WhichPlayer.Knuth,
                description: "Knuth checks in deal 3, Knuth get lost and has 4 cards in next deal")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(3))

            .GetCards(WhichPlayer.Knuth, new DealNumber(4))
            .GetCards(WhichPlayer.Graham, new DealNumber(4))
            .BidTwoPairs(WhichPlayer.Graham, FaceCard.Nine, FaceCard.Ten,
                description: "Graham bids two pairs Nine and Ten in deal 4")
            .BidFourOfAKind(WhichPlayer.Knuth, FaceCard.Nine,
                description: "Knuth bids four of a kind Nine in deal 4")
            .Check(WhichPlayer.Graham,
                description: "Graham checks in deal 4, Knuth get lost and has 5 cards in next deal")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(4))

            .GetCards(WhichPlayer.Knuth, new DealNumber(5))
            .GetCards(WhichPlayer.Graham, new DealNumber(5))
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Queen, description: "Knuth bids high card Queen in deal 5")
            .BidHighCard(WhichPlayer.Graham, FaceCard.King, "Graham bids high card King in deal 5")
            .BidPair(WhichPlayer.Knuth, FaceCard.Queen, "Knuth bids pair Queen in deal 5")
            .BidPair(WhichPlayer.Graham, FaceCard.King, "Graham bids pair King in deal 5")
            .BidFullHouse(WhichPlayer.Knuth, FaceCard.Ace, FaceCard.King,
                description: "Knuth bids full house Ace and King in deal 5")
            .Check(WhichPlayer.Graham,
                description: "Graham checks in deal 5 and Knuth get lost the game!")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(5))

            .Build();

        await Verify(results);
    }
}