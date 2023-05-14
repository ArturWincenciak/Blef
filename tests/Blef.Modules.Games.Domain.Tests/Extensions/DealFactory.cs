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
        var playerGuid_1 = Guid.Parse("D657F6EE-C629-415C-AB9F-595FAF33D401");
        var playerGuid_2 = Guid.Parse("AC10CA94-AC44-4004-9F03-6B1EA2FEB1BD");
        var playerHand_1 = new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)});
        var playerHand_2 = new Hand(new Card[] {new(FaceCard.King, Suit.Diamonds)});
        var player_1 = new DealPlayer(Player: new PlayerId(playerGuid_1), playerHand_1);
        var player_2 = new DealPlayer(Player: new PlayerId(playerGuid_2), playerHand_2);
        var players = new[] {player_1, player_2};
        var moveSequence = new MoveSequence(new[]
        {
            new Move(player_1.Player, Order: Order.Create(1)),
            new Move(player_2.Player, Order: Order.Create(2))
        });
        var deal = new Deal(dealId, dealSet: new DealSet(playersSet: new DealPlayersSet(players), moveSequence));

        return (deal, player_1, player_2);
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
        var playerGuid_1 = Guid.Parse("54C03C56-A602-43E4-B922-CF17456FDA55");
        var playerGuid_2 = Guid.Parse("DBE9B64F-0EC9-4A4F-A357-A482A09F3567");
        var playerGuid_3 = Guid.Parse("D4C01AE4-C790-41EB-BF9E-DA2CC05D7A61");
        var playerGuid_4 = Guid.Parse("59EB6325-6D26-477A-BA2F-BE0C1EF43521");
        var player_1 = new DealPlayer(Player: new PlayerId(playerGuid_1), withFirstPlayerHand);
        var player_2 = new DealPlayer(Player: new PlayerId(playerGuid_2), withSecondPlayerHand);
        var player_3 = new DealPlayer(Player: new PlayerId(playerGuid_3), withThirdPlayerHand);
        var player_4 = new DealPlayer(Player: new PlayerId(playerGuid_4), withFourthPlayerHand);
        var players = new[] {player_1, player_2, player_3, player_4};
        var moveSequence = new MoveSequence(new[]
        {
            new Move(player_1.Player, Order: Order.Create(1)),
            new Move(player_2.Player, Order: Order.Create(2)),
            new Move(player_3.Player, Order: Order.Create(3)),
            new Move(player_4.Player, Order: Order.Create(4))
        });
        var deal = new Deal(dealId, dealSet: new DealSet(playersSet: new DealPlayersSet(players), moveSequence));

        return (deal, player_1, player_2, player_3, player_4);
    }

    public static (
        Deal Deal,
        DealPlayer First,
        DealPlayer Second,
        DealPlayer Third,
        DealPlayer Fourth)
        GivenDealWithFourPlayers() =>
        GivenDealWithFourPlayers(
            withFirstPlayerHand: new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)}),
            withSecondPlayerHand: new Hand(new Card[] {new(FaceCard.King, Suit.Diamonds)}),
            withThirdPlayerHand: new Hand(new Card[] {new(FaceCard.Queen, Suit.Diamonds)}),
            withFourthPlayerHand: new Hand(new Card[] {new(FaceCard.Jack, Suit.Diamonds)}));
}