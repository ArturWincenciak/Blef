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
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Nine)
            .Check(WhichPlayer.Graham)
            .GetGameFlow()
            .GetDealFlow(new DealNumber(1))

            .GetCards(WhichPlayer.Knuth, new DealNumber(2))
            .GetCards(WhichPlayer.Graham, new DealNumber(2))
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(new DealNumber(2))

            .GetCards(WhichPlayer.Knuth, new DealNumber(3))
            .GetCards(WhichPlayer.Graham, new DealNumber(3))
            .BidPair(WhichPlayer.Knuth, FaceCard.King)
            .BidPair(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(new DealNumber(3))

            .GetCards(WhichPlayer.Knuth, new DealNumber(4))
            .GetCards(WhichPlayer.Graham, new DealNumber(4))
            .BidTwoPairs(WhichPlayer.Graham, FaceCard.Nine, FaceCard.Ten)
            .BidFourOfAKind(WhichPlayer.Knuth, FaceCard.Nine)
            .Check(WhichPlayer.Graham)
            .GetGameFlow()
            .GetDealFlow(new DealNumber(4))

            .GetCards(WhichPlayer.Knuth, new DealNumber(5))
            .GetCards(WhichPlayer.Graham, new DealNumber(5))
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Queen)
            .BidHighCard(WhichPlayer.Graham, FaceCard.King)
            .BidPair(WhichPlayer.Knuth, FaceCard.Queen)
            .BidPair(WhichPlayer.Graham, FaceCard.King)
            .BidFullHouse(WhichPlayer.Knuth, FaceCard.Ace, FaceCard.King)
            .Check(WhichPlayer.Graham)
            .GetGameFlow()
            .GetDealFlow(new DealNumber(5))

            .Build();

        await Verify(results);
    }
}