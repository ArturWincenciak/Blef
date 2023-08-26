using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

[UsesVerify]
public class TreePlayersPlayTheGame
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
            .NewDeal()
            .GetGameFlow()
            .GetDealFlow(new DealNumber(1))

            .GetCards(WhichPlayer.Knuth, new DealNumber(1))
            .GetCards(WhichPlayer.Graham, new DealNumber(1))
            .GetCards(WhichPlayer.Conway, new DealNumber(1))
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Nine)
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ten)
            .BidHighCard(WhichPlayer.Conway, FaceCard.Jack)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(1))

            .GetCards(WhichPlayer.Knuth, new DealNumber(2))
            .GetCards(WhichPlayer.Graham, new DealNumber(2))
            .GetCards(WhichPlayer.Conway, new DealNumber(2))
            .BidPair(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Conway)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(2))

            .GetCards(WhichPlayer.Knuth, new DealNumber(3))
            .GetCards(WhichPlayer.Graham, new DealNumber(3))
            .GetCards(WhichPlayer.Conway, new DealNumber(3))
            .BidPair(WhichPlayer.Conway, FaceCard.King)
            .BidThreeOfAKind(WhichPlayer.Knuth, FaceCard.Ace)
            .Check(WhichPlayer.Graham)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(3))

            .GetCards(WhichPlayer.Knuth, new DealNumber(4))
            .GetCards(WhichPlayer.Graham, new DealNumber(4))
            .GetCards(WhichPlayer.Conway, new DealNumber(4))
            .BidFourOfAKind(WhichPlayer.Knuth, FaceCard.Nine)
            .BidStraightFlush(WhichPlayer.Graham, Suit.Clubs)
            .Check(WhichPlayer.Conway)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(4))

            .GetCards(WhichPlayer.Knuth, new DealNumber(5))
            .GetCards(WhichPlayer.Graham, new DealNumber(5))
            .GetCards(WhichPlayer.Conway, new DealNumber(5))
            .BidHighCard(WhichPlayer.Graham, FaceCard.Queen)
            .BidHighCard(WhichPlayer.Conway, FaceCard.King)
            .BidPair(WhichPlayer.Knuth, FaceCard.Queen)
            .BidPair(WhichPlayer.Graham, FaceCard.King)
            .BidFullHouse(WhichPlayer.Conway, FaceCard.Ace, FaceCard.King)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(5))

            .GetCards(WhichPlayer.Knuth, new DealNumber(6))
            .GetCards(WhichPlayer.Graham, new DealNumber(6))
            .GetCards(WhichPlayer.Conway, new DealNumber(6))
            .BidHighCard(WhichPlayer.Conway, FaceCard.Nine)
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ten)
            .BidHighCard(WhichPlayer.Graham, FaceCard.Jack)
            .BidPair(WhichPlayer.Conway, FaceCard.Nine)
            .BidPair(WhichPlayer.Knuth, FaceCard.Ten)
            .BidPair(WhichPlayer.Graham, FaceCard.Jack)
            .BidThreeOfAKind(WhichPlayer.Conway, FaceCard.Queen)
            .BidThreeOfAKind(WhichPlayer.Knuth, FaceCard.King)
            .BidThreeOfAKind(WhichPlayer.Graham, FaceCard.Ace)
            .BidStraightFlush(WhichPlayer.Conway, Suit.Clubs)
            .BidStraightFlush(WhichPlayer.Knuth, Suit.Diamonds)
            .BidStraightFlush(WhichPlayer.Graham, Suit.Hearts)
            .BidRoyalFlush(WhichPlayer.Conway, Suit.Clubs)
            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Diamonds)
            .BidRoyalFlush(WhichPlayer.Graham, Suit.Hearts)
            .Check(WhichPlayer.Conway)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(6))

            .GetCards(WhichPlayer.Knuth, new DealNumber(7))
            .GetCards(WhichPlayer.Graham, new DealNumber(7))
            .GetCards(WhichPlayer.Conway, new DealNumber(7))
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Queen)
            .BidHighCard(WhichPlayer.Graham, FaceCard.King)
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace)
            .BidPair(WhichPlayer.Knuth, FaceCard.Ace)
            .BidLowStraight(WhichPlayer.Graham)
            .BidHighStraight(WhichPlayer.Conway)
            .BidFullHouse(WhichPlayer.Knuth, FaceCard.King, FaceCard.Ace)
            .BidFullHouse(WhichPlayer.Graham, FaceCard.Ace, FaceCard.King)
            .BidFourOfAKind(WhichPlayer.Conway, FaceCard.King)
            .BidFourOfAKind(WhichPlayer.Knuth, FaceCard.Ace)
            .Check(WhichPlayer.Graham)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(7))

            .GetCards(WhichPlayer.Knuth, new DealNumber(8))
            .GetCards(WhichPlayer.Graham, new DealNumber(8))
            .GetCards(WhichPlayer.Conway, new DealNumber(8))
            .BidFullHouse(WhichPlayer.Graham, FaceCard.Ace, FaceCard.King)
            .Check(WhichPlayer.Conway)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(8))

            .GetCards(WhichPlayer.Knuth, new DealNumber(9))
            .GetCards(WhichPlayer.Graham, new DealNumber(9))
            .GetCards(WhichPlayer.Conway, new DealNumber(9))
            .BidHighStraight(WhichPlayer.Conway)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(9))

            .GetCards(WhichPlayer.Knuth, new DealNumber(10))
            .GetCards(WhichPlayer.Graham, new DealNumber(10))
            .GetCards(WhichPlayer.Conway, new DealNumber(10))
            .BidLowStraight(WhichPlayer.Knuth)
            .BidHighStraight(WhichPlayer.Graham)
            .BidFullHouse(WhichPlayer.Conway, FaceCard.Ace, FaceCard.King)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(10))

            .GetCards(WhichPlayer.Knuth, new DealNumber(11))
            .GetCards(WhichPlayer.Graham, new DealNumber(11))
            .GetCards(WhichPlayer.Conway, new DealNumber(11))
            .BidFourOfAKind(WhichPlayer.Graham, FaceCard.King)
            .Check(WhichPlayer.Conway)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(11))

            .GetCards(WhichPlayer.Knuth, new DealNumber(12))
            .GetCards(WhichPlayer.Graham, new DealNumber(12))
            .GetCards(WhichPlayer.Conway, new DealNumber(12))
            .BidLowStraight(WhichPlayer.Conway)
            .BidHighStraight(WhichPlayer.Knuth)
            .Check(WhichPlayer.Graham)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(12))

            .GetCards(WhichPlayer.Knuth, new DealNumber(13))
            .GetCards(WhichPlayer.Graham, new DealNumber(13))
            .GetCards(WhichPlayer.Conway, new DealNumber(13))
            .BidFourOfAKind(WhichPlayer.Knuth, FaceCard.King)
            .Check(WhichPlayer.Graham)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(13))

            .GetCards(WhichPlayer.Knuth, new DealNumber(14))
            .GetCards(WhichPlayer.Graham, new DealNumber(14))
            .GetCards(WhichPlayer.Conway, new DealNumber(14))

            // todo: change type of that bad request, player joined but already lost the game
/*

No: 135,
Request: {
  Path: games-module/gameplays/Guid_1/players/Guid_3/deals/14/cards,
  Method: Get
},
Response: {
  StatusCode: BadRequest,
  Payload: {
    Type: .../doc/problem-details/player-not-joined-the-game.md,
    Status: 400,
    Title: Player not joined the game,
    Detail: Player 'PlayerId { Id = Guid_3 }' not joined the game 'Guid_1',
    Instance: /games-module/gameplays/Guid_1/players/Guid_3/deals/14/cards
  }
}

*/
            .BidFourOfAKind(WhichPlayer.Conway, FaceCard.King)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(14))

            .Build();

        await Verify(results);
    }
}