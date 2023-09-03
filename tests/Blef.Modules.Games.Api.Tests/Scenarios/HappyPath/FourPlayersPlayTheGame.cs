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

            .Build();

        await Verify(results);
    }
}