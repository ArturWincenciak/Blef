using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

[UsesVerify]
public class ThreePlayersNextMove
{
    [Fact]
    public async Task GrahamLostTheGameInDealWhichTheConwayStartsAndNextMoveHasKnuth()
    {
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Graham get lost the game in deal which the Conway starts the deal and the next move has Knuth. " +
                "The next deal would be started by the Graham if he had not lost, " +
                "so the next deal is started by the next player after the current one who lost.")

            .NewGame("Create a new game.")
            .JoinPlayer(WhichPlayer.Conway, description: "Join Conway as a first player.")
            .JoinPlayer(WhichPlayer.Graham, description: "Join Graham as a second player.")
            .JoinPlayer(WhichPlayer.Knuth, description: "Join Knuth as a third player.")
            .NewDeal("Start the first deal.")

            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "First move by Conway in deal 1.")
            .Check(WhichPlayer.Graham, description: "Graham get lost fist deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Graham, Suit.Spades, description: "First move by Graham in deal 2.")
            .Check(WhichPlayer.Knuth, description: "Graham get lost second deal (+1 card, has 3 cards).")

            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace, description: "First move by Knuth in deal 3.")
            .BidPair(WhichPlayer.Conway, FaceCard.Ace, description: "Next move by Conway in deal 3.")
            .Check(WhichPlayer.Graham, description: "Graham get lost third deal (+1 card, has 4 cards).")

            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "First move by Conway in deal 4.")
            .Check(WhichPlayer.Graham, description: "Graham get lost fourth deal (+1 card, has 5 cards).")

            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "First move by Graham in deal 5.")
            .Check(WhichPlayer.Knuth, description: "Temporary Knuth get lost a deal (+1 card, has 2 cards).")

            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace, description: "First move by Knuth in deal 6.")
            .Check(WhichPlayer.Conway, description: "Temporary Conway get lost a deal (+1 card, has 2 cards).")

            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "First move by Conway in deal 7.")
            .Check(WhichPlayer.Graham, description: "Graham GET LOST DEAL AND THE GAME in deal 7.")

            .BidHighCard(
                WhichPlayer.Knuth, FaceCard.Ace,
                description: "Act and assert: in the deal number 8 first move should be made by Knuth.")

            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task KnuthLostTheGameInDealWhichTheGrahamStartsAndNextMoveHasConway()
    {
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Knuth get lost the game in deal which the Graham starts the deal and the next move has Conway. " +
                "The next deal would be started by the Knuth if he had not lost, " +
                "so the next deal is started by the next player after the current one who lost.")

            .NewGame("Create a new game.")
            .JoinPlayer(WhichPlayer.Conway, description: "Join Conway as a first player.")
            .JoinPlayer(WhichPlayer.Graham, description: "Join Graham as a second player.")
            .JoinPlayer(WhichPlayer.Knuth, description: "Join Knuth as a third player.")
            .NewDeal("Start the first deal.")

            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "First move by Conway in deal 1.")
            .BidPair(WhichPlayer.Graham, FaceCard.Ace, description: "Next move by Graham in deal 1.")
            .Check(WhichPlayer.Knuth, description: "Knuth get lost fist deal (+1 card, has 2 cards).")

            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "First move by Graham in deal 2.")
            .Check(WhichPlayer.Knuth, description: "Knuth get lost second deal (+1 card, has 3 cards).")

            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Spades, description: "First move by Knuth in deal 3.")
            .Check(WhichPlayer.Conway, description: "Knuth get lost third deal (+1 card, has 4 cards).")

            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "First move by Conway in deal 4.")
            .BidPair(WhichPlayer.Graham, FaceCard.Ace, description: "Next move by Graham in deal 4.")
            .Check(WhichPlayer.Knuth, description: "Knuth get lost fourth deal (+1 card, has 5 cards).")

            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "First move by Graham in deal 5.")
            .Check(WhichPlayer.Knuth, description: "Knuth GET LOST DEAL AND THE GAME in deal 5.")

            .BidHighCard(
                WhichPlayer.Conway, FaceCard.Ace,
                description: "Act and assert: in the deal number 6 first move should be made by Conway.")

            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task ConwayLostTheGameInDealWhichTheKnuthAndNextMoveHasGraham()
    {
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Conway get lost the game in deal which the Knuth starts the deal and the next move has Graham." +
                "The next deal would be started by the Conway if he had not lost, " +
                "so the next deal is started by the next player after the current one who lost.")

            .NewGame("Create a new game.")
            .JoinPlayer(WhichPlayer.Conway, description: "Join Conway as a first player.")
            .JoinPlayer(WhichPlayer.Graham, description: "Join Graham as a second player.")
            .JoinPlayer(WhichPlayer.Knuth, description: "Join Knuth as a third player.")
            .NewDeal("Start the first deal.")

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Spades, description: "First move by Conway in deal 1.")
            .Check(WhichPlayer.Graham, description: "Conway get lost fist deal (+1 card, has 2 cards).")

            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "First move by Graham in deal 2.")
            .BidPair(WhichPlayer.Knuth, FaceCard.Ace, description: "Next move by Knuth in deal 2.")
            .Check(WhichPlayer.Conway, description: "Conway get lost second deal (+1 card, has 3 cards).")

            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace, description: "First move by Knuth in deal 3.")
            .Check(WhichPlayer.Conway, description: "Conway get lost third deal (+1 card, has 4 cards).")

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Spades, description: "First move by Conway in deal 4.")
            .Check(WhichPlayer.Graham, description: "Conway get lost fourth deal (+1 card, has 5 cards).")

            .BidRoyalFlush(WhichPlayer.Graham, Suit.Spades, description: "First move by Graham in deal 5.")
            .Check(WhichPlayer.Knuth, description: "Temporary Knuth get lost a deal (+1 card, has 2 cards).")

            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace, description: "First move by Knuth in deal 6.")
            .Check(WhichPlayer.Conway, description: "Conway GET LOST DEAL AND THE GAME in deal 6.")

            .BidHighCard(
                WhichPlayer.Graham, FaceCard.Ace,
                description: "Act and assert: in the deal number 7 first move should be made by Graham.")

            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task GrahamLostTheGameAndNextMoveHasKnuth()
    {
        // act
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Graham get lost the game in deal which the Graham starts the deal and the next move has Knuth.")
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .NewDeal()

            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "First move by Conway in deal 1.")
            .Check(WhichPlayer.Graham, description: "Graham get lost first deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Graham, Suit.Spades, description: "First move by Graham in deal 2.")
            .Check(WhichPlayer.Knuth, description: "Graham get lost second deal (+1 card, has 3 cards).")

            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace, description: "First move by Knuth in deal 3.")
            .BidPair(WhichPlayer.Conway, FaceCard.Ace, description: "Next move by Conway in deal 3.")
            .Check(WhichPlayer.Graham, description: "Graham get lost third deal (+1 card, has 4 cards).")

            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "First move by Conway in deal 4.")
            .Check(WhichPlayer.Graham, description: "Graham get lost fourth deal (+1 card, has 5 cards).")

            .BidRoyalFlush(WhichPlayer.Graham, Suit.Diamonds, description: "First move by Graham in deal 5.")
            .Check(WhichPlayer.Knuth, description: "Graham GET LOST DEAL AND THE GAME in deal 5.")

            .BidHighCard(
                WhichPlayer.Knuth, FaceCard.Ace,
                description: "Act and assert: in the deal number 6 first move should be made by Knuth.")

            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task KnuthLostTheGameAndNextMoveHasConway()
    {
        // act
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Knuth get lost the game in deal which the Knuth starts the deal and the next move has Conway.")
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .NewDeal()

            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "First move by Conway in deal 1.")
            .Check(WhichPlayer.Graham, description: "Graham get lost fist deal (+1 card, has 2 cards).")

            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "First move by Graham in deal 2.")
            .Check(WhichPlayer.Knuth, description: "Knuth get lost second deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Spades, description: "First move by Knuth in deal 3.")
            .Check(WhichPlayer.Conway, description: "Knuth get lost third deal (+1 card, has 3 cards).")

            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "First move by Conway in deal 4.")
            .BidPair(WhichPlayer.Graham, FaceCard.Ace, description: "Next move by Graham in deal 4.")
            .Check(WhichPlayer.Knuth, description: "Knuth get lost fourth deal (+1 card, has 4 cards).")

            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "First move by Graham in deal 5.")
            .Check(WhichPlayer.Knuth, description: "Knuth get lost fifth deal (+1 card, has 5 cards).")

            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Spades, description: "First move by Knuth in deal 6.")
            .Check(WhichPlayer.Conway, description: "Knuth GET LOST DEAL AND THE GAME in deal 6.")

            .BidHighCard(
                WhichPlayer.Conway, FaceCard.Ace,
                description: "Act and assert: in the deal number 7 first move should be made by Conway.")

            .Build();

        // assert
        await Verify(results);
    }

    [Fact]
    public async Task ConwayLostTheGameAndNextMoveHasGraham()
    {
        // act
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Conway get lost the game in deal which the Conway starts the deal and the next move has Graham.")
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .NewDeal()

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Spades, "First move by Conway in deal 1.")
            .Check(WhichPlayer.Graham, "Conway get lost fist deal (+1 card, has 2 cards).")

            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, "First move by Graham in deal 2.")
            .BidPair(WhichPlayer.Knuth, FaceCard.Ace, "Next move by Knuth in deal 2.")
            .Check(WhichPlayer.Conway, "Conway get lost second deal (+1 card, has 3 cards).")

            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace, "First move by Knuth in deal 3.")
            .Check(WhichPlayer.Conway, "Conway get lost third deal (+1 card, has 4 cards).")

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Spades, "First move by Conway in deal 4.")
            .Check(WhichPlayer.Graham, "Conway get lost fourth deal (+1 card, has 5 cards).")

            .BidRoyalFlush(WhichPlayer.Graham, Suit.Spades, "First move by Graham in deal 5.")
            .Check(WhichPlayer.Knuth, "Graham get lost fifth deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Spades, "First move by Knuth in deal 6.")
            .Check(WhichPlayer.Conway, "Knuth get lost sixth deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Spades, "First move by Conway in deal 7.")
            .Check(WhichPlayer.Graham, "Conway GET LOST DEAL AND THE GAME in deal 7.")

            .BidHighCard(
                WhichPlayer.Graham, FaceCard.Ace,
                "Act and assert: in the deal number 8 first move should be made by Graham.")

            .Build();

        // assert
        await Verify(results);
    }
}