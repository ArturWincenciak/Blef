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
            .GetCards(WhichPlayer.Knuth, deal: new DealNumber(1), description: "Knuth has one card")
            .GetCards(WhichPlayer.Graham, deal: new DealNumber(1), description: "Graham has one card")
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Nine, description: "Knuth starts the deal")
            .Check(WhichPlayer.Graham, description: "Graham checks, Knuth get lost the deal)")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(1))
            .GetCards(WhichPlayer.Knuth, deal: new DealNumber(2), description: "Knuth has two cards")
            .GetCards(WhichPlayer.Graham, deal: new DealNumber(2), description: "Graham has one card")
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "Graham starts the deal")
            .Check(WhichPlayer.Knuth, description: "Knuth checks and get lost the deal")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(2))
            .GetCards(WhichPlayer.Knuth, deal: new DealNumber(3), description: "Knuth has three cards")
            .GetCards(WhichPlayer.Graham, deal: new DealNumber(3), description: "Graham has one card")
            .BidPair(WhichPlayer.Knuth, FaceCard.King, description: "Bad move Knuth!")
            .BidPair(WhichPlayer.Graham, FaceCard.Ace, description: "Graham starts the deal")
            .Check(WhichPlayer.Knuth, description: "Knuth checks and get lost the deal")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(3))
            .GetCards(WhichPlayer.Knuth, deal: new DealNumber(4), description: "Knuth has four cards")
            .GetCards(WhichPlayer.Graham, deal: new DealNumber(4), description: "Graham has one card")
            .BidTwoPairs(WhichPlayer.Graham, FaceCard.Nine, FaceCard.Ten, description: "Graham starts the deal")
            .BidFourOfAKind(WhichPlayer.Knuth, FaceCard.Nine)
            .Check(WhichPlayer.Graham, description: "Graham checks and Knuth get lost the deal")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(4))
            .GetCards(WhichPlayer.Knuth, deal: new DealNumber(5), description: "Knuth has five cards")
            .GetCards(WhichPlayer.Graham, deal: new DealNumber(5), description: "Graham has one card")
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Queen, description: "Bad move Knuth!")
            .BidHighCard(WhichPlayer.Graham, FaceCard.King, description: "Graham starts the deal")
            .BidPair(WhichPlayer.Knuth, FaceCard.Queen)
            .BidPair(WhichPlayer.Graham, FaceCard.King)
            .BidFullHouse(WhichPlayer.Knuth, FaceCard.Ace, FaceCard.King)
            .Check(WhichPlayer.Graham, description: "Graham checks and Knuth get lost the GAME!")
            .GetGameFlow("Graham wins the game!")
            .GetDealFlow(new DealNumber(5))
            .Build();

        await Verify(results);
    }
}