using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

[UsesVerify]
public class NextMove
{
    [Fact]
    public async Task Scenario1()
    {
        // graham lost the game in deal which the conway starts the deal and next move has knuth - GREEN

        // act
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .NewDeal()

            // deal 1 - move: conway - lost graham - +1 card (2)
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace)
            .Check(WhichPlayer.Graham)

            // deal 2 - move: graham - lost graham - +1 card (3)
            .BidRoyalFlush(WhichPlayer.Graham, Suit.Spades)
            .Check(WhichPlayer.Knuth)

            // deal 3 - move: knuth - lost graham - +1 card (4)
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace)
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .Check(WhichPlayer.Graham)

            // deal 4 - move: conway - lost graham - +1 card (5)
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace)
            .Check(WhichPlayer.Graham)

            // deal 5 - move: graham - lost knuth - +1 card (2)
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)

            // deal 6 - move: knuth - lost conway - +1 card (2)
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace)
            .Check(WhichPlayer.Conway)

            // deal 7 - move: conway - lost graham - lost the game
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace)
            .Check(WhichPlayer.Graham)

            // deal 8 - next move is knuth - not conway
            .GetDealFlow(new DealNumber(7))
            .GetDealFlow(new DealNumber(8))
            .GetGameFlow()
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace)

            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task Scenario2()
    {
        // knuth lost the game in deal which the graham starts the deal and next move has conway - RED

        // act
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .NewDeal()

            // deal 1 - move: conway - lost knuth - +1 card (2)
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace)
            .BidPair(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)

            // deal 2 - move: graham - lost knuth - +1 card (3)
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)

            // deal 3 - move: knuth - lost knuth - +1 card (4)
            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Spades)
            .Check(WhichPlayer.Conway)

            // deal 4 - move: conway - lost knuth - +1 card (5)
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace)
            .BidPair(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)

            // deal 5 - move: graham - lost knuth - lost the game
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)

            // deal 6 - next move is conway
            .GetDealFlow(new DealNumber(5))
            .GetDealFlow(new DealNumber(6))
            .GetGameFlow()
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace)

            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task GrahamLostTheGameAndNextMoveHasKnuth()
    {
        // graham lost the game in deal which the graham starts the deal and next move has knuth - GREEN

        // act
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .NewDeal()

            // deal 1 - move: conway - lost graham - +1 card (2)
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace)
            .Check(WhichPlayer.Graham)

            // deal 2 - move: graham - lost graham - +1 card (3)
            .BidRoyalFlush(WhichPlayer.Graham, Suit.Spades)
            .Check(WhichPlayer.Knuth)

            // deal 3 - move: knuth - lost graham - +1 card (4)
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace)
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .Check(WhichPlayer.Graham)

            // deal 4 - move: conway - lost graham - +1 card (5)
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace)
            .Check(WhichPlayer.Graham)

            // deal 5 - move: graham - lost graham - lost the game
            .BidRoyalFlush(WhichPlayer.Graham, Suit.Diamonds)
            .Check(WhichPlayer.Knuth)

            // deal 6 - next move is knuth
            .GetDealFlow(new DealNumber(5))
            .GetDealFlow(new DealNumber(6))
            .GetGameFlow()
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace)

            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task KnuthLostTheGameAndNextMoveHasConway()
    {
        // knuth lost the game in deal which the knuth starts the deal and next move has conway - GREEN

        // act
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .NewDeal()

            // deal 1 - move: conway - lost graham - +1 card (2)
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace)
            .Check(WhichPlayer.Graham)

            // deal 2 - move: graham - lost knuth - +1 card (2)
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)

            // deal 3 - move: knuth - lost knuth - +1 card (3)
            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Spades)
            .Check(WhichPlayer.Conway)

            // deal 4 - move: conway - lost knuth - +1 card (4)
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace)
            .BidPair(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)

            // deal 5 - move: graham - lost knuth - +1 card (5)
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)

            // deal 6 - move: knuth - lost knuth - lost the game
            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Spades)
            .Check(WhichPlayer.Conway)

            // deal 7 - next move is conway
            .GetDealFlow(new DealNumber(6))
            .GetDealFlow(new DealNumber(7))
            .GetGameFlow()
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace)

            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task ConwayLostTheGameAndNextMoveHasGraham()
    {
        // conway lost the game in deal which the conway starts the deal and next move has graham - RED

        // act
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .NewDeal()

            // deal 1 - move: conway - lost conway - +1 card (2)
            .BidRoyalFlush(WhichPlayer.Conway, Suit.Spades)
            .Check(WhichPlayer.Graham)

            // deal 2 - move: graham - lost conway - +1 card (3)
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace)
            .BidPair(WhichPlayer.Knuth, FaceCard.Ace)
            .Check(WhichPlayer.Conway)

            // deal 3 - move: knuth - lost conway - +1 card (4)
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace)
            .Check(WhichPlayer.Conway)

            // deal 4 - move: conway - lost conway - +1 card (5)
            .BidRoyalFlush(WhichPlayer.Conway, Suit.Spades)
            .Check(WhichPlayer.Graham)

            // deal 5 - move: graham - lost graham - +1 card (2)
            .BidRoyalFlush(WhichPlayer.Graham, Suit.Spades)
            .Check(WhichPlayer.Knuth)

            // deal 6 - move: knuth - lost knuth - +1 card (2)
            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Spades)
            .Check(WhichPlayer.Conway)

            // deal 7 - move: conway - lost conway - lost the game
            .BidRoyalFlush(WhichPlayer.Conway, Suit.Spades)
            .Check(WhichPlayer.Graham)

            // deal 8 - next move is graham
            .GetDealFlow(new DealNumber(7))
            .GetDealFlow(new DealNumber(8))
            .GetGameFlow()
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace)

            .Build();

        // assert
        await Verify(results);
    }
}