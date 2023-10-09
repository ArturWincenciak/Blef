using System.ComponentModel;
using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

[UsesVerify]
public class FourPlayersPlayTheGame
{
    [Fact]
    public async Task Scenario()
    {
        var results = await new TestBuilder()

            .NewGame()
            .GetGameFlow()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Riemann)
            .NewDeal()
            .GetGameFlow()
            .GetDealFlow(new DealNumber(1))

            .GetCards(WhichPlayer.Knuth, new DealNumber(1))
            .GetCards(WhichPlayer.Graham, new DealNumber(1))
            .GetCards(WhichPlayer.Conway, new DealNumber(1))
            .GetCards(WhichPlayer.Riemann, new DealNumber(1))
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Nine)
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ten)
            .BidHighCard(WhichPlayer.Conway, FaceCard.Jack)
            .BidHighCard(WhichPlayer.Riemann, FaceCard.Queen)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(1))

            .GetCards(WhichPlayer.Knuth, new DealNumber(2))
            .GetCards(WhichPlayer.Graham, new DealNumber(2))
            .GetCards(WhichPlayer.Conway, new DealNumber(2))
            .GetCards(WhichPlayer.Riemann, new DealNumber(2))
            .BidPair(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Conway)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(2))

            .GetCards(WhichPlayer.Knuth, new DealNumber(3))
            .GetCards(WhichPlayer.Graham, new DealNumber(3))
            .GetCards(WhichPlayer.Conway, new DealNumber(3))
            .GetCards(WhichPlayer.Riemann, new DealNumber(3))
            .BidPair(WhichPlayer.Conway, FaceCard.King)
            .BidThreeOfAKind(WhichPlayer.Knuth, FaceCard.Ace)
            .BidThreeOfAKind(WhichPlayer.Riemann, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(3))

            .GetCards(WhichPlayer.Knuth, new DealNumber(4))
            .GetCards(WhichPlayer.Graham, new DealNumber(4))
            .GetCards(WhichPlayer.Conway, new DealNumber(4))
            .GetCards(WhichPlayer.Riemann, new DealNumber(4))
            .BidFourOfAKind(WhichPlayer.Knuth, FaceCard.Nine)
            .BidStraightFlush(WhichPlayer.Riemann, Suit.Clubs)
            .Check(WhichPlayer.Conway)
            .BidHighStraight(WhichPlayer.Knuth)
            .Check(WhichPlayer.Graham)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(4))

            .GetCards(WhichPlayer.Knuth, new DealNumber(5))
            .GetCards(WhichPlayer.Graham, new DealNumber(5))
            .GetCards(WhichPlayer.Conway, new DealNumber(5))
            .GetCards(WhichPlayer.Riemann, new DealNumber(5))
            .BidStraightFlush(WhichPlayer.Knuth, Suit.Clubs)
            .Check(WhichPlayer.Graham)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(5))

            .GetCards(WhichPlayer.Knuth, new DealNumber(6))
            .GetCards(WhichPlayer.Graham, new DealNumber(6))
            .GetCards(WhichPlayer.Conway, new DealNumber(6))
            .GetCards(WhichPlayer.Riemann, new DealNumber(6))
            .BidHighStraight(WhichPlayer.Graham)
            .Check(WhichPlayer.Conway)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(6))

            .GetCards(WhichPlayer.Knuth, new DealNumber(7))
            .GetCards(WhichPlayer.Graham, new DealNumber(7))
            .GetCards(WhichPlayer.Conway, new DealNumber(7))
            .GetCards(WhichPlayer.Riemann, new DealNumber(7))
            .BidFullHouse(WhichPlayer.Conway, FaceCard.Ace, FaceCard.King)
            .Check(WhichPlayer.Riemann)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(7))

            .GetCards(WhichPlayer.Knuth, new DealNumber(8))
            .GetCards(WhichPlayer.Graham, new DealNumber(8))
            .GetCards(WhichPlayer.Conway, new DealNumber(8))
            .GetCards(WhichPlayer.Riemann, new DealNumber(8))
            .BidTwoPairs(WhichPlayer.Riemann, FaceCard.Ace, FaceCard.King)
            .BidTwoPairs(WhichPlayer.Knuth, FaceCard.King, FaceCard.Ace)
            .BidThreeOfAKind(WhichPlayer.Knuth, FaceCard.King)
            .BidThreeOfAKind(WhichPlayer.Graham, FaceCard.Ace)
            .BidFourOfAKind(WhichPlayer.Conway, FaceCard.King)
            .BidFourOfAKind(WhichPlayer.Riemann, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(8))

            .GetCards(WhichPlayer.Knuth, new DealNumber(9), description: "Knuth has two cards")
            .GetCards(WhichPlayer.Graham, new DealNumber(9), description: "Graham has one card")
            .GetCards(WhichPlayer.Conway, new DealNumber(9), description: "Conway has three cards")
            .GetCards(WhichPlayer.Riemann, new DealNumber(9), description: "Riemann get lost the game!")
            .BidTwoPairs(WhichPlayer.Knuth, FaceCard.Ace, FaceCard.King, description: "Knuth starts the deal")
            .BidTwoPairs(WhichPlayer.Graham, FaceCard.Ace, FaceCard.King, description: "Bad move Graham!")
            .BidTwoPairs(WhichPlayer.Conway, FaceCard.Ace, FaceCard.King, description: "Bad move Conway!")
            .Check(WhichPlayer.Knuth, description: "Bad move Knuth!")
            .Check(WhichPlayer.Graham, description: "Graham checks the bid, Knuth get lost the deal")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(9))

            .GetCards(WhichPlayer.Knuth, new DealNumber(10), description: "Knuth has three cards")
            .GetCards(WhichPlayer.Graham, new DealNumber(10), description: "Graham has one card")
            .GetCards(WhichPlayer.Conway, new DealNumber(10), description: "Conway has three cards")
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "Graham starts the deal")
            .Check(WhichPlayer.Conway, "Conway checks the bid, Conway get lost the deal")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(10))

            .GetCards(WhichPlayer.Knuth, new DealNumber(11), description: "Knuth has three cards")
            .GetCards(WhichPlayer.Graham, new DealNumber(11), description: "Graham has one card")
            .GetCards(WhichPlayer.Conway, new DealNumber(11), description: "Conway has four cards")
            .BidFlush(WhichPlayer.Conway, Suit.Clubs, description: "Conway starts the deal")
            .Check(WhichPlayer.Knuth, description: "Knuth checks the bid, Conway get lost the deal")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(11))

            .GetCards(WhichPlayer.Knuth, new DealNumber(12), description: "Knuth has three cards")
            .GetCards(WhichPlayer.Graham, new DealNumber(12), description: "Graham has one card")
            .GetCards(WhichPlayer.Conway, new DealNumber(12), description: "Conway has five cards")
            .BidHighStraight(WhichPlayer.Knuth, description: "Knuth starts the deal")
            .Check(WhichPlayer.Graham, description: "Graham checks the bid, Graham get lost the deal")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(12))

            .GetCards(WhichPlayer.Knuth, new DealNumber(13), description: "Knuth has three cards")
            .GetCards(WhichPlayer.Graham, new DealNumber(13), description: "Graham has two cards")
            .GetCards(WhichPlayer.Conway, new DealNumber(13), description: "Conway has five cards")
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "Graham starts the deal")
            .Check(WhichPlayer.Conway, description: "Conway checks the bid, Graham get LOST THE GAME!")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(13))

            // todo: there is a bug, this turn should be started by Conway but algorithm starts it by Knuth

            .Build();

        await Verify(results);
    }
}