using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.RockyRoad;

public class RockyRoadTests
{
    private static TestBuilder Arrange => new TestBuilder()
        .NewGame()
        .JoinPlayer(WhichPlayer.Conway)
        .JoinPlayer(WhichPlayer.Graham)
        .NewDeal();

    [Fact]
    public async Task BidIsNotHigherThenLastOneTest()
    {
        var results = await Arrange
            .BidFullHouse(WhichPlayer.Conway, FaceCard.Ace, FaceCard.King)
            .BidPair(WhichPlayer.Graham, FaceCard.Ace)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task GameAlreadyStartedTest()
    {
        var results = await Arrange
            .NewDeal()
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task GameNotStartedTest()
    {
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task JoinGameAlreadyStartedTest()
    {
        var results = await Arrange
            .JoinPlayer(WhichPlayer.Knuth)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task NoBidToCheckTest()
    {
        var results = await Arrange
            .Check(WhichPlayer.Conway)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task NoBidToCheckOnLastDealFinishedTest()
    {
        var results = await Arrange
            .BidFlush(WhichPlayer.Conway, Suit.Hearts)
            .Check(WhichPlayer.Graham)
            .Check(WhichPlayer.Graham)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task NoPlayersNotEnoughPlayersTest()
    {
        var results = await new TestBuilder()
            .NewGame()
            .NewDeal()
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task OnePlayerIsNotEnoughPlayersTest()
    {
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .NewDeal()
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task PlayerAlreadyJoinedTest()
    {
        var results = new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Conway)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task PlayerAlreadyJoinedWithOtherPlayerTest()
    {
        var results = new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Conway)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task WhenBidThatIsNotThisPlayerTurnNowTest()
    {
        var results = await Arrange
            .BidPair(WhichPlayer.Graham, FaceCard.Ace)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task WhenCheckThatIsNotThisPlayerTurnNowTest()
    {
        var results = await Arrange
            .Check(WhichPlayer.Graham)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task WhenTwoBidsThatIsNotThisPlayerTurnNowTest()
    {
        var results = await Arrange
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task WhenBidAndCheckThatPlayerIsNotThisPlayerTurnNowTest()
    {
        var results = await Arrange
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .Check(WhichPlayer.Conway)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task WhenBidCheckAndBidPlayerIsNotThisPlayerTurnNowTest()
    {
        var results = await Arrange
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .Check(WhichPlayer.Graham)
            .BidPair(WhichPlayer.Graham, FaceCard.Ace)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task BidNotJoinedPlayer()
    {
        // arrange
        var notJoinedPlayer = new PlayerId(Guid.Parse("53D4523A-4004-4E31-98ED-CA1C5A909AB9"));

        // act
        var results = await Arrange
            .BidHighCard(notJoinedPlayer, FaceCard.Ace)
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task CheckNotJoinedPlayer()
    {
        // arrange
        var notJoinedPlayer = new PlayerId(Guid.Parse("8FCD458B-DBD7-4BFD-A3F9-926A7E9827C2"));

        // act
        var results = await Arrange
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace)
            .Check(notJoinedPlayer)
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task GetNotJoinedPlayerCardsTest()
    {
        // arrange
        var notJoinedPlayer = new PlayerId(Guid.Parse("B0651086-59DE-43EE-B7CC-ED394F1F81C2"));

        // act
        var results = await Arrange
            .GetCards(notJoinedPlayer, deal: new(1))
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task GetPlayerCardsFromNotExistedDealTest()
    {
        // arrange
        var notExistedDeal = new DealNumber(100);

        // act
        var results = await Arrange
            .GetCards(WhichPlayer.Conway, notExistedDeal)
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task TooManyPlayersTest()
    {
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Riemann)
            .JoinPlayer(WhichPlayer.Planck)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task BidPlayerThatLostLastDealTest()
    {
        // arrange
        var arrangeGame = new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .NewDeal();

        var conwayLostFirstDeal = arrangeGame
            .BidRoyalFlush(WhichPlayer.Conway, Suit.Hearts)
            .Check(WhichPlayer.Graham);

        var conwayLostSecondDeal = conwayLostFirstDeal
            .BidPair(WhichPlayer.Graham, FaceCard.Nine)
            .BidPair(WhichPlayer.Knuth, FaceCard.Ten)
            .BidRoyalFlush(WhichPlayer.Conway, Suit.Hearts)
            .Check(WhichPlayer.Graham);

        var conwayLostThirdDeal = conwayLostSecondDeal
            .BidPair(WhichPlayer.Graham, FaceCard.Nine)
            .BidPair(WhichPlayer.Knuth, FaceCard.Ten)
            .BidRoyalFlush(WhichPlayer.Conway, Suit.Hearts)
            .Check(WhichPlayer.Graham);

        var conwayLostFourthDeal = conwayLostThirdDeal
            .BidHighCard(WhichPlayer.Graham, FaceCard.Nine)
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace)
            .Check(WhichPlayer.Conway);

        var conwayLostTheGame = conwayLostFourthDeal
            .BidPair(WhichPlayer.Graham, FaceCard.Nine)
            .BidPair(WhichPlayer.Knuth, FaceCard.Ten)
            .BidRoyalFlush(WhichPlayer.Conway, Suit.Hearts)
            .Check(WhichPlayer.Graham);

        var getGame = conwayLostTheGame
            .GetGameFlow()
            .GetDealFlow(new(6));

        // act
        var results = await getGame
            .BidPair(WhichPlayer.Graham, FaceCard.Nine)
            .BidPair(WhichPlayer.Knuth, FaceCard.Ten)
            .BidRoyalFlush(WhichPlayer.Conway, Suit.Hearts)
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task GameOverTest()
    {
        // arrange
        var arrangeGame = new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .NewDeal();

        var conwayLostFirstDeal = arrangeGame
            .BidRoyalFlush(WhichPlayer.Conway, Suit.Hearts)
            .Check(WhichPlayer.Graham, description: "Conway lost the first deal");

        var conwayLostSecondDeal = conwayLostFirstDeal
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Conway, description: "Conway lost the second deal");

        var conwayLostThirdDeal = conwayLostSecondDeal
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Conway, description: "Conway lost the third deal");

        var conwayLostFourthDeal = conwayLostThirdDeal
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Conway, description: "Conway lost the fourth deal");

        var conwayLostTheGame = conwayLostFourthDeal
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Conway, description: "Conway lost the game");

        var getGame = conwayLostTheGame
            .GetGameFlow()
            .GetDealFlow(new(5));

        // act
        var results = await getGame
            .BidPair(WhichPlayer.Conway, FaceCard.Nine, description: "Conway cannot bid in the game over")
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task GetNotExistedGameFlowTest()
    {
        // arrange
        var notExistedGame = new GameId(Guid.Parse("E3450C4B-A874-4EBC-A60A-B0DE539EA75D"));

        // act
        var results = await new TestBuilder()
            .GetGameFlow(notExistedGame)
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task GetNotExistedDealTest()
    {
        // arrange
        const int notExistedDeal = 100;
        var arrangeGame = new TestBuilder()
            .NewGame();

        // act
        var results = await arrangeGame
            .GetDealFlow(new(notExistedDeal))
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task GetDealOfNotExistedGameTest()
    {
        // arrange
        var notExistedGame = new GameId(Guid.Parse("38BE0303-E5FA-4D2A-AE20-C8559F247073"));

        // act
        var results = await new TestBuilder()
            .GetDealFlow(notExistedGame, deal: new(1))
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task BidOnNotExistedGameTest()
    {
        // arrange
        var notExistedGame = new GameId(Guid.Parse("8DCF0098-B0A6-4466-9162-9C7968C811E1"));

        // act
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .BidHighCard(notExistedGame, WhichPlayer.Conway, FaceCard.Ace)
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task CheckOnNotExistedGameTest()
    {
        // arrange
        var notExistedGame = new GameId(Guid.Parse("250C6364-5AED-47A7-809C-2FD7DDE255E5"));

        // act
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .Check(notExistedGame, WhichPlayer.Conway)
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task JoinPlayerToNotExistedGameTest()
    {
        // arrange
        var notExistedGame = new GameId(Guid.Parse("F2F0F0F2-0F0F-0F0F-0F0F-0F0F0F0F0F0F"));

        // act
        var results = await new TestBuilder()
            .JoinPlayer(notExistedGame, WhichPlayer.Conway)
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task NewDealOnNotExistedGameTest()
    {
        // arrange
        var notExistedGame = new GameId(Guid.Parse("53A9A321-453B-422B-B2C2-A2A6B2C8EACA"));

        // act
        var results = await new TestBuilder()
            .NewDeal(notExistedGame)
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task IfBeforeBidHasBeenPlacedWithWrongPokerHandThenTheSamePlayerHaveToBidOnceAgain()
    {
        // act
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Conway)
            .NewDeal()
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace)
            .BidHighCard(WhichPlayer.Conway, FaceCard.King)
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task CannotCheckOutOfOrderPolicyAndWhenTheDealHasNotBeenStarted()
    {
        // act
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Conway)
            .NewDeal()
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace, description: "Knuth starts the first deal")
            .Check(WhichPlayer.Knuth, description: "That is not this player move")
            .Check(WhichPlayer.Conway, description: "Conway check first deal")
            .GetDealFlow(deal: new(1), description: "Conway lost the deal, second deal has been started")
            .Check(WhichPlayer.Conway, description: "In the second deal that is not this player move")
            .Check(WhichPlayer.Knuth, description: "In the second deal there is no bid to check")
            .BidHighCard(WhichPlayer.Knuth, FaceCard.King, description: "Knuth starts the second deal")
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "Conway placed the bid in the second deal")
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task NextDealNotStarted()
    {
        // act
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .NewDeal()
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .Check(WhichPlayer.Graham)
            .Check(WhichPlayer.Conway)
            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task GetPlayerCardsFromNotExistedGame()
    {
        // arrange
        var notExistedGame = new GameId(Guid.Parse("88810D25-3D66-4B3A-A45F-D55E976DA547"));

        // act
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .NewDeal()
            .GetCards(notExistedGame, WhichPlayer.Conway, deal: new(1))
            .Build();

        // assert
        await Verify(results);
    }
}