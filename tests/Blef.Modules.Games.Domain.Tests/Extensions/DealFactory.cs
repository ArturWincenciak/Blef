using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests.Extensions;

internal static class DealFactory
{
    public static (
        Deal Deal,
        DealPlayer First, DealPlayer Second) GivenDealWithTwoPlayers()
    {
        var dealNumber = new DealNumber(1);
        var gameGuid = Guid.Parse("1A2C67F4-CDF5-4664-A4E7-2CF74E341401");
        var gameId = new GameId(gameGuid);
        var dealId = new DealId(gameId, dealNumber);
        var playerGuid1 = Guid.Parse("D657F6EE-C629-415C-AB9F-595FAF33D401");
        var playerGuid2 = Guid.Parse("AC10CA94-AC44-4004-9F03-6B1EA2FEB1BD");
        var playerHand1 = new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)});
        var playerHand2 = new Hand(new Card[] {new(FaceCard.King, Suit.Diamonds)});
        var player1 = new DealPlayer(Player: new(playerGuid1), playerHand1);
        var player2 = new DealPlayer(Player: new(playerGuid2), playerHand2);
        var players = new[] {player1, player2};
        var moveSequence = new MoveSequence(new[]
        {
            new Move(player1.Player, Order: Order.Create(1)),
            new Move(player2.Player, Order: Order.Create(2))
        });
        var deal = new Deal(dealId, dealSet: new(playersSet: new(players), moveSequence));

        return (deal, player1, player2);
    }

    public static (
        Deal Deal,
        DealPlayer First,
        DealPlayer Second,
        DealPlayer Third,
        DealPlayer Fourth)
        GivenDealWithFourPlayers(
            Hand withFirstPlayerHand,
            Hand withSecondPlayerHand,
            Hand withThirdPlayerHand,
            Hand withFourthPlayerHand)
    {
        var dealNumber = new DealNumber(1);
        var gameGuid = Guid.Parse("F0C56541-DFDE-45B5-9A6F-FBD8CBE127D1");
        var gameId = new GameId(gameGuid);
        var dealId = new DealId(gameId, dealNumber);
        var playerGuid1 = Guid.Parse("54C03C56-A602-43E4-B922-CF17456FDA55");
        var playerGuid2 = Guid.Parse("DBE9B64F-0EC9-4A4F-A357-A482A09F3567");
        var playerGuid3 = Guid.Parse("D4C01AE4-C790-41EB-BF9E-DA2CC05D7A61");
        var playerGuid4 = Guid.Parse("59EB6325-6D26-477A-BA2F-BE0C1EF43521");
        var player1 = new DealPlayer(Player: new(playerGuid1), withFirstPlayerHand);
        var player2 = new DealPlayer(Player: new(playerGuid2), withSecondPlayerHand);
        var player3 = new DealPlayer(Player: new(playerGuid3), withThirdPlayerHand);
        var player4 = new DealPlayer(Player: new(playerGuid4), withFourthPlayerHand);
        var players = new[] {player1, player2, player3, player4};
        var moveSequence = new MoveSequence(new[]
        {
            new Move(player1.Player, Order: Order.Create(1)),
            new Move(player2.Player, Order: Order.Create(2)),
            new Move(player3.Player, Order: Order.Create(3)),
            new Move(player4.Player, Order: Order.Create(4))
        });
        var deal = new Deal(dealId, dealSet: new(playersSet: new(players), moveSequence));

        return (deal, player1, player2, player3, player4);
    }

    public static (
        Deal Deal,
        DealPlayer First,
        DealPlayer Second,
        DealPlayer Third,
        DealPlayer Fourth)
        GivenDealWithFourPlayers() =>
        GivenDealWithFourPlayers(
            withFirstPlayerHand: new(new Card[] {new(FaceCard.Ace, Suit.Clubs)}),
            withSecondPlayerHand: new(new Card[] {new(FaceCard.King, Suit.Diamonds)}),
            withThirdPlayerHand: new(new Card[] {new(FaceCard.Queen, Suit.Diamonds)}),
            withFourthPlayerHand: new(new Card[] {new(FaceCard.Jack, Suit.Diamonds)}));
}