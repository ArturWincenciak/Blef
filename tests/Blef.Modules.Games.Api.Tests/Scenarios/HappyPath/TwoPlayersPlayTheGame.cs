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
            .JoinPlayer(WhichPlayer.Planck)
            .NewDeal()
            .GetGameFlow()
            .GetDealFlow(new DealNumber(1))
            .GetCards(WhichPlayer.Knuth, deal: new DealNumber(1), description: "Knuth has one card")
            .GetCards(WhichPlayer.Planck, deal: new DealNumber(1), description: "Planck has one card")
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Nine, description: "Knuth starts the deal")
            .Check(WhichPlayer.Planck, description: "Planck checks, Knuth get lost the deal)")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(1))
            .GetCards(WhichPlayer.Knuth, deal: new DealNumber(2), description: "Knuth has two cards")
            .GetCards(WhichPlayer.Planck, deal: new DealNumber(2), description: "Planck has one card")
            .BidHighCard(WhichPlayer.Planck, FaceCard.Ace, description: "Planck starts the deal")
            .Check(WhichPlayer.Knuth, description: "Knuth checks and get lost the deal")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(2))
            .GetCards(WhichPlayer.Knuth, deal: new DealNumber(3), description: "Knuth has three cards")
            .GetCards(WhichPlayer.Planck, deal: new DealNumber(3), description: "Planck has one card")
            .BidPair(WhichPlayer.Knuth, FaceCard.King, description: "Bad move Knuth!")
            .BidPair(WhichPlayer.Planck, FaceCard.Ace, description: "Planck starts the deal")
            .Check(WhichPlayer.Knuth, description: "Knuth checks and get lost the deal")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(3))
            .GetCards(WhichPlayer.Knuth, deal: new DealNumber(4), description: "Knuth has four cards")
            .GetCards(WhichPlayer.Planck, deal: new DealNumber(4), description: "Planck has one card")
            .BidTwoPairs(WhichPlayer.Planck, FaceCard.Nine, FaceCard.Ten, description: "Planck starts the deal")
            .BidFourOfAKind(WhichPlayer.Knuth, FaceCard.Nine)
            .Check(WhichPlayer.Planck, description: "Planck checks and Knuth get lost the deal")
            .GetGameFlow()
            .GetDealFlow(new DealNumber(4))
            .GetCards(WhichPlayer.Knuth, deal: new DealNumber(5), description: "Knuth has five cards")
            .GetCards(WhichPlayer.Planck, deal: new DealNumber(5), description: "Planck has one card")
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Queen, description: "Bad move Knuth!")
            .BidHighCard(WhichPlayer.Planck, FaceCard.King, description: "Planck starts the deal")
            .BidPair(WhichPlayer.Knuth, FaceCard.Queen)
            .BidPair(WhichPlayer.Planck, FaceCard.King)
            .BidFullHouse(WhichPlayer.Knuth, FaceCard.Ace, FaceCard.King)
            .Check(WhichPlayer.Planck, description: "Planck checks and Knuth get lost the GAME!")
            .GetGameFlow("Planck wins the game!")
            .GetDealFlow(new DealNumber(5))
            .Build();

        await Verify(results);
    }
}