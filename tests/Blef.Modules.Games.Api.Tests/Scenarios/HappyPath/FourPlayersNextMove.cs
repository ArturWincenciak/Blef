using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

[UsesVerify]
public class FourPlayersNextMove
{
    [Fact] // todo: RED
    public async Task ConwayLostTheGameAndNextMoveHasGraham()
    {
        // act
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Conway get lost the game in deal which the Conway starts the deal and the next move has Graham")
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Planck)
            .NewDeal()

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Clubs, description: "First move by Conway in deal 1")
            .Check(WhichPlayer.Graham, description: "Conway get lost first deal (+1 card, has 2 cards).")

            .BidHighCard(WhichPlayer.Graham, FaceCard.Nine, description: "First move by Graham in deal 2.")
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ten, description: "Next move by Knuth in deal 2.")
            .BidHighCard(WhichPlayer.Planck, FaceCard.Ace, description: "Next move by Planck in deal 2.")
            .Check(WhichPlayer.Conway, description: "Graham get lost second deal (+1 card, has 3 cards).")

            .BidHighCard(WhichPlayer.Knuth, FaceCard.Nine, description: "First move by Knuth in deal 3.")
            .BidHighCard(WhichPlayer.Planck, FaceCard.Ace, description: "Next move by Planck in deal 3.")
            .Check(WhichPlayer.Conway, description: "Knuth get lost third deal (+1 card, has 4 cards).")

            .BidHighCard(WhichPlayer.Planck, FaceCard.Ace, description: "First move by Planck in deal 4.")
            .Check(WhichPlayer.Conway, description: "Planck get lost fourth deal (+1 card, has 5 cards).")

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Clubs, description: "First move by Conway in deal 5.")
            .Check(WhichPlayer.Graham, description: "Conway GET LOST DEAL AND THE GAME in deal 6.")

            .BidHighCard(
                WhichPlayer.Graham, FaceCard.Ace,
                description: "Act and assert: in deal number 7 fist move should be by Graham.")

            .Build();

        // assert
        await Verify(results);
    }

    [Fact] // todo: RED
    public async Task GrahamLostTheGameAndNextMoveHasKnuth()
    {
        // act
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Graham get lost the game in deal which the Graham starts the deal and the next move has Knuth")
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Planck)
            .NewDeal()

            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "First move by Conway in deal 1.")
            .Check(WhichPlayer.Graham, description: "Graham get lost first deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Graham, Suit.Clubs, description: "First move by Graham in deal 2.")
            .Check(WhichPlayer.Knuth, description: "Graham get lost second deal (+1 card, has 3 cards).")

            .BidHighCard(WhichPlayer.Knuth, FaceCard.Nine, description: "First move by Knuth in deal 3.")
            .BidHighCard(WhichPlayer.Planck, FaceCard.Ten, description: "Next move by Planck in deal 3.")
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "Next move by Conway in deal 3.")
            .Check(WhichPlayer.Graham, description: "Graham get lost third deal (+1 card, has 4 cards).")

            .BidHighCard(WhichPlayer.Planck, FaceCard.Nine, description: "First move by Planck in deal 4.")
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "Next move by Conway in deal 4.")
            .Check(WhichPlayer.Graham, description: "Planck get lost fourth deal (+1 card, has 5 cards).")

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Clubs, description: "First move by Conway in deal 5.")
            .Check(WhichPlayer.Graham, "Conway get lost fifth deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Graham, Suit.Clubs, description: "First move by Graham in deal 6.")
            .Check(WhichPlayer.Knuth, description: "Graham GET LOST DEAL AND THE GAME in deal 6.")

            .BidHighCard(
                WhichPlayer.Knuth, FaceCard.Ace,
                description: "Act and assert: in deal number 8 fist move should be by Knuth.")

            .Build();

        // assert
        await Verify(results);
    }

    [Fact] // todo: RED
    public async Task KnuthLostTheGameAndNextMoveHasPlanck()
    {
        // act
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Knuth get lost the game in deal which the Knuth starts the deal and the next move has Planck")
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Planck)
            .NewDeal()

            .BidHighCard(WhichPlayer.Conway, FaceCard.Nine, description: "First move by Conway in deal 1.")
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "Next move by Graham in deal 1.")
            .Check(WhichPlayer.Knuth, description: "Knuth get lost first deal (+1 card, has 2 cards).")

            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "First move by Graham in deal 2.")
            .Check(WhichPlayer.Knuth, description: "Knuth get lost second deal (+1 card, has 3 cards).")

            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Clubs, description: "First move by Knuth in deal 3.")
            .Check(WhichPlayer.Planck, description: "Knuth get lost third deal (+1 card, has 4 cards).")

            .BidHighCard(WhichPlayer.Planck, FaceCard.Nine, description: "First move by Planck in deal 4.")
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ten, description: "Next move by Conway in deal 4.")
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "Next move by Graham in deal 4.")
            .Check(WhichPlayer.Knuth, description: "Knuth get lost fourth deal (+1 card, has 5 cards).")

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Clubs, description: "First move by Conway in deal 5.")
            .Check(WhichPlayer.Graham, description: "Conway get lost fifth deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Graham, Suit.Clubs, description: "First move by Graham in deal 6.")
            .Check(WhichPlayer.Knuth, "Graham get lost sixth deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Clubs, description: "First move by Knuth in deal 7.")
            .Check(WhichPlayer.Planck, description: "Knuth GET LOST DEAL AND THE GAME in deal 7.")

            .BidHighCard(
                WhichPlayer.Planck, FaceCard.Ace,
                description: "Act and assert: in deal number 8 fist move should be by Planck.")

            .Build();

        // assert
        await Verify(results);
    }

    [Fact] // todo: RED
    public async Task PlanckLostTheGameAndNextMoveHasConway()
    {
        // act
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Planck get lost the game in deal which the Planck starts the deal and the next move has Conway")
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Planck)
            .NewDeal()

            .BidHighCard(WhichPlayer.Conway, FaceCard.Nine, description: "First move by Conway in deal 1.")
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ten, description: "Next move by Graham in deal 1.")
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace, description: "Next move by Knuth in deal 1.")
            .Check(WhichPlayer.Planck, description: "Planck get lost first deal (+1 card, has 2 cards).")

            .BidHighCard(WhichPlayer.Graham, FaceCard.Nine, description: "First move by Graham in deal 2.")
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace, description: "Next move by Knuth in deal 2.")
            .Check(WhichPlayer.Planck, description: "Planck get lost second deal (+1 card, has 3 cards).")

            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace, description: "First move by Knuth in deal 3.")
            .Check(WhichPlayer.Planck, description: "Knuth get lost third deal (+1 card, has 4 cards).")

            .BidRoyalFlush(WhichPlayer.Planck, Suit.Clubs, description: "First move by Planck in deal 4.")
            .Check(WhichPlayer.Conway, description: "Planck get lost fourth deal (+1 card, has 5 cards).")

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Clubs, "First move by Conway in deal 5.")
            .Check(WhichPlayer.Graham, description: "Conway get lost fifth deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Graham, Suit.Clubs, "First move by Graham in deal 6.")
            .Check(WhichPlayer.Knuth, description: "Graham get lost sixth deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Clubs, "First move by Knuth in deal 7.")
            .Check(WhichPlayer.Planck, description: "Knuth get lost seventh deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Planck, Suit.Clubs, "First move by Planck in deal 8.")
            .Check(WhichPlayer.Conway, description: "Planck GET LOST DEAL AND THE GAME in deal 8.")

            .BidHighCard(
                WhichPlayer.Conway, FaceCard.Ace,
                description: "Act and assert: in deal number 9 fist move should be by Conway.")

            .Build();

        // assert
        await Verify(results);
    }

    [Fact] // todo: RED
    public async Task GrahamLostTheGameInDealWhichConwayStartsAndNextMoveHasKnuth()
    {
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Graham get lost the game in deal which the Conway starts the deal and the next move has Knuth. " +
                "The next deal would be started by the Graham if he had not lost, " +
                "so the next deal is started by the next player after the current one who lost.")

            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Planck)
            .NewDeal()

            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "First move by Conway in deal 1.")
            .Check(WhichPlayer.Graham, description: "Conway get lost first deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Graham, Suit.Clubs, description: "First move by Graham in deal 2.")
            .Check(WhichPlayer.Knuth, description: "Graham get lost second deal (+1 card, has 3 cards).")

            .BidHighCard(WhichPlayer.Knuth, FaceCard.Nine, description: "First move by Knuth in deal 3.")
            .BidHighCard(WhichPlayer.Planck, FaceCard.Ten, description: "Next move by Planck in deal 3.")
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "Next move by Conway in deal 3.")
            .Check(WhichPlayer.Graham, description: "Graham get lost third deal (+1 card, has 4 cards).")

            .BidHighCard(WhichPlayer.Planck, FaceCard.Nine, description: "First move by Planck in deal 4.")
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ace, description: "Next move by Conway in deal 4.")
            .Check(WhichPlayer.Graham, description: "Graham get lost fourth deal (+1 card, has 5 cards).")

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Clubs, description: "First move by Conway in deal 5.")
            .Check(WhichPlayer.Graham, description: "Conway get lost fifth deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Graham, Suit.Clubs, description: "First move by Graham in deal 6.")
            .Check(WhichPlayer.Knuth, description: "Graham GET LOST DEAL AND THE GAME in deal 6.")

            .BidHighCard(
                WhichPlayer.Knuth, FaceCard.Ace,
                description: "Act and assert: in deal number 7 fist move should be by Knuth.")

            .Build();

        await Verify(results);
    }

    [Fact] // todo: RED
    public async Task KnuthLostTheGameInDealWhichGrahamStartsAndNextMoveHasPlanck()
    {
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Knuth get lost the game in deal which the Graham starts the deal and the next move has Planck. " +
                "The next deal would be started by the Knuth if he had not lost, " +
                "so the next deal is started by the next player after the current one who lost.")

            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Planck)
            .NewDeal()

            .BidHighCard(WhichPlayer.Conway, FaceCard.Nine, description: "First move by Conway in deal 1.")
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "Next move by Graham in deal 1.")
            .Check(WhichPlayer.Knuth, description: "Knuth get lost first deal (+1 card, has 2 cards).")

            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "First move by Graham in deal 2.")
            .Check(WhichPlayer.Knuth, description: "Knuth get lost second deal (+1 card, has 3 cards).")

            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Clubs, description: "First move by Knuth in deal 3.")
            .Check(WhichPlayer.Planck, description: "Knuth get lost third deal (+1 card, has 4 cards).")

            .BidHighCard(WhichPlayer.Planck, FaceCard.Nine, description: "First move by Planck in deal 4.")
            .BidHighCard(WhichPlayer.Conway, FaceCard.Ten, description: "Next move by Conway in deal 4.")
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ace, description: "Next move by Graham in deal 4.")
            .Check(WhichPlayer.Knuth, description: "Knuth get lost fourth deal (+1 card, has 5 cards).")

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Clubs, description: "First move by Conway in deal 5.")
            .Check(WhichPlayer.Graham, description: "Conway get lost fifth deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Graham, Suit.Clubs, description: "First move by Graham in deal 6.")
            .Check(WhichPlayer.Knuth, description: "Graham get lost sixth deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Clubs, description: "First move by Knuth in deal 7.")
            .Check(WhichPlayer.Planck, description: "Knuth GET LOST DEAL AND THE GAME in deal 7.")

            .BidHighCard(
                WhichPlayer.Planck, FaceCard.Ace,
                description: "Act and assert: in deal number 8 fist move should be by Planck.")

            .Build();

        await Verify(results);
    }

    [Fact] // todo: RED
    public async Task PlankLostTheGameInDealWhichKnuthStartsAndNextMoveHasConway()
    {
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Planck get lost the game in deal which the Knuth starts the deal and the next move has Conway. " +
                "The next deal would be started by the Planck if he had not lost, " +
                "so the next deal is started by the next player after the current one who lost.")

            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Planck)
            .NewDeal()

            .BidHighCard(WhichPlayer.Conway, FaceCard.Nine, description: "First move by Conway in deal 1.")
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ten, description: "Next move by Graham in deal 1.")
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace, description: "Next move by Knuth in deal 1.")
            .Check(WhichPlayer.Planck, description: "Planck get lost first deal (+1 card, has 2 cards).")

            .BidHighCard(WhichPlayer.Graham, FaceCard.Nine, description: "First move by Graham in deal 2.")
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace, description: "Next move by Knuth in deal 2.")
            .Check(WhichPlayer.Planck, description: "Planck get lost second deal (+1 card, has 3 cards).")

            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ace, description: "First move by Knuth in deal 3.")
            .Check(WhichPlayer.Planck, description: "Knuth get lost third deal (+1 card, has 4 cards).")

            .BidRoyalFlush(WhichPlayer.Planck, Suit.Clubs, description: "First move by Planck in deal 4.")
            .Check(WhichPlayer.Conway, description: "Planck get lost fourth deal (+1 card, has 5 cards).")

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Clubs, description: "First move by Conway in deal 5.")
            .Check(WhichPlayer.Graham, description: "Conway get lost fifth deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Graham, Suit.Clubs, description: "First move by Graham in deal 6.")
            .Check(WhichPlayer.Knuth, description: "Graham get lost sixth deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Knuth, Suit.Clubs, description: "First move by Knuth in deal 7.")
            .Check(WhichPlayer.Planck, description: "Knuth get lost seventh deal (+1 card, has 2 cards).")

            .BidRoyalFlush(WhichPlayer.Planck, Suit.Clubs, description: "First move by Planck in deal 8.")
            .Check(WhichPlayer.Conway, description: "Planck GET LOST DEAL AND THE GAME in deal 8.")

            .BidHighCard(
                WhichPlayer.Conway, FaceCard.Ace,
                description: "Act and assert: in deal number 9 fist move should be by Conway.")

            .Build();

        await Verify(results);
    }

    [Fact] // todo: RED
    public async Task ConwayLostTheGameInDealWhichPlankStartsAndNextMoveHasGraham()
    {
        var results = await new TestBuilder()
            .WithScenarioDescription(
                "Conway get lost the game in deal which the Planck starts the deal and the next move has Graham. " +
                "The next deal would be started by the Conway if he had not lost, " +
                "so the next deal is started by the next player after the current one who lost.")

            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Planck)
            .NewDeal()

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Clubs, description: "First move by Conway in deal 1.")
            .Check(WhichPlayer.Graham, description: "Conway get lost first deal (+1 card, has 2 cards).")

            .BidHighCard(WhichPlayer.Graham, FaceCard.Nine, description: "First move by Graham in deal 2.")
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Ten, description: "Next move by Knuth in deal 2.")
            .BidHighCard(WhichPlayer.Planck, FaceCard.Ace, description: "Next move by Planck in deal 2.")
            .Check(WhichPlayer.Conway, description: "Graham get lost second deal (+1 card, has 3 cards).")

            .BidHighCard(WhichPlayer.Knuth, FaceCard.Nine, description: "First move by Knuth in deal 3.")
            .BidHighCard(WhichPlayer.Planck, FaceCard.Ace, description: "Next move by Planck in deal 3.")
            .Check(WhichPlayer.Conway, description: "Knuth get lost third deal (+1 card, has 4 cards).")

            .BidHighCard(WhichPlayer.Planck, FaceCard.Ace, description: "First move by Planck in deal 4.")
            .Check(WhichPlayer.Conway, description: "Planck get lost fourth deal (+1 card, has 5 cards).")

            .BidRoyalFlush(WhichPlayer.Conway, Suit.Clubs, description: "First move by Conway in deal 5.")
            .Check(WhichPlayer.Graham, description: "Conway GET LOST DEAL AND THE GAME in deal 6.")

            .BidHighCard(
                WhichPlayer.Graham, FaceCard.Ace,
                description: "Act and assert: in deal number 7 fist move should be by Graham.")

            .Build();

        await Verify(results);
    }
}