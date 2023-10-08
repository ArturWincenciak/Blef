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
            .NewGame()
            .WithScenarioDescription(
                "Conway get lost the game in deal which the Conway starts the deal and the next move has Graham")
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
}