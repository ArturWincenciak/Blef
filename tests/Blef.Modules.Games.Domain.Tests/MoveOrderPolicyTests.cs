using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Services;

namespace Blef.Modules.Games.Domain.Tests;

public class MoveOrderPolicyTests
{
    [Fact]
    public void CreateMoveOrderPolicyTest()
    {
        var exception = Record.Exception(() =>
        {
            GivenPolicyWithTwoPlayers();
            GivenPolicyWithThreePlayers();
            GivenPolicyWithFourPlayers();
        });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void CannotCreateWithNullSequenceTest() =>
        Assert.Throws<ArgumentNullException>(() => new MoveOrderPolicy(null!));

    [Fact]
    public void MoveWithTwoPlayersTest()
    {
        // arrange
        var (policy, player1, player2) = GivenPolicyWithTwoPlayers();

        // act
        var exception = Record.Exception(() =>
        {
            policy.CheckMoveOrder(player1);
            policy.CommitMove(player1);
            policy.CheckMoveOrder(player2);
            policy.CommitMove(player2);
            policy.CheckMoveOrder(player1);
            policy.CommitMove(player1);
            policy.CheckMoveOrder(player2);
            policy.CommitMove(player2);
            policy.CheckMoveOrder(player1);
            policy.CommitMove(player1);
        });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void MoveWithThreePlayersTest()
    {
        // arrange
        var (policy, player1, player2, player3) = GivenPolicyWithThreePlayers();

        // act
        var exception = Record.Exception(() =>
        {
            policy.CheckMoveOrder(player1);
            policy.CommitMove(player1);
            policy.CheckMoveOrder(player2);
            policy.CommitMove(player2);
            policy.CheckMoveOrder(player3);
            policy.CommitMove(player3);
            policy.CheckMoveOrder(player1);
            policy.CommitMove(player1);
            policy.CheckMoveOrder(player2);
            policy.CommitMove(player2);
            policy.CheckMoveOrder(player3);
            policy.CommitMove(player3);
            policy.CheckMoveOrder(player1);
            policy.CommitMove(player1);
        });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void MoveWithFourPlayersTest()
    {
        // arrange
        var (policy, player1, player2, player3, player4) = GivenPolicyWithFourPlayers();

        // act
        var exception = Record.Exception(() =>
        {
            policy.CheckMoveOrder(player1);
            policy.CommitMove(player1);
            policy.CheckMoveOrder(player2);
            policy.CommitMove(player2);
            policy.CheckMoveOrder(player3);
            policy.CommitMove(player3);
            policy.CheckMoveOrder(player4);
            policy.CommitMove(player4);
            policy.CheckMoveOrder(player1);
            policy.CommitMove(player1);
            policy.CheckMoveOrder(player2);
            policy.CommitMove(player2);
            policy.CheckMoveOrder(player3);
            policy.CommitMove(player3);
            policy.CheckMoveOrder(player4);
            policy.CommitMove(player4);
            policy.CheckMoveOrder(player1);
            policy.CommitMove(player1);
        });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void SecondPlayerCannotMakeFirstMoveTest()
    {
        // arrange
        var (policy, _, player2) = GivenPolicyWithTwoPlayers();

        // act
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() => { policy.CheckMoveOrder(player2); });
    }

    [Fact]
    public void FirstPlayerCannotMakeTwoMovesInRowTest()
    {
        // arrange
        var (policy, player1, _) = GivenPolicyWithTwoPlayers();

        // act
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
        {
            policy.CheckMoveOrder(player1);
            policy.CommitMove(player1);
            policy.CheckMoveOrder(player1);
        });
    }

    [Fact]
    public void SecondPlayerCannotMakeTwoMovesInRowTest()
    {
        // arrange
        var (policy, player1, player2) = GivenPolicyWithTwoPlayers();

        // act
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
        {
            policy.CheckMoveOrder(player1);
            policy.CheckMoveOrder(player2);
            policy.CheckMoveOrder(player2);
        });
    }

    [Fact]
    public void ThirdPlayerCannotMakeSecondMoveTest()
    {
        // arrange
        var (policy, player1, _, player3) = GivenPolicyWithThreePlayers();

        // act
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
        {
            policy.CheckMoveOrder(player1);
            policy.CheckMoveOrder(player3);
        });
    }

    [Fact]
    public void FirstPlayerCannotMakeFourthMoveTest()
    {
        // arrange
        var (policy, player1, player2, player3, _) = GivenPolicyWithFourPlayers();

        // act
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
        {
            policy.CheckMoveOrder(player1);
            policy.CheckMoveOrder(player2);
            policy.CheckMoveOrder(player3);
            policy.CheckMoveOrder(player1);
        });
    }

    [Fact]
    public void SecondPlayerCannotMakeFirstMoveInSecondRoundTest()
    {
        // arrange
        var (policy, player1, player2, player3, player4) = GivenPolicyWithFourPlayers();

        // act
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
        {
            policy.CheckMoveOrder(player1);
            policy.CheckMoveOrder(player2);
            policy.CheckMoveOrder(player3);
            policy.CheckMoveOrder(player4);
            policy.CheckMoveOrder(player2);
        });
    }

    [Fact]
    public void ThirdPlayerCannotMakeFirstMoveInSecondRoundTest()
    {
        // arrange
        var (policy, player1, player2, player3, player4) = GivenPolicyWithFourPlayers();

        // act
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
        {
            policy.CheckMoveOrder(player1);
            policy.CheckMoveOrder(player2);
            policy.CheckMoveOrder(player3);
            policy.CheckMoveOrder(player4);
            policy.CheckMoveOrder(player3);
        });
    }

    [Fact]
    public void FourthPlayerCannotMakeFirstMoveInSecondRoundTest()
    {
        // arrange
        var (policy, player1, player2, player3, player4) = GivenPolicyWithFourPlayers();

        // act
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
        {
            policy.CheckMoveOrder(player1);
            policy.CheckMoveOrder(player2);
            policy.CheckMoveOrder(player3);
            policy.CheckMoveOrder(player4);
            policy.CheckMoveOrder(player4);
        });
    }

    private static (
        MoveOrderPolicy Policy,
        PlayerId First, PlayerId Second) GivenPolicyWithTwoPlayers()
    {
        var playerId1 = new PlayerId(Guid.Parse("1C963163-046E-4DC4-8DAD-34C0AA557F6A"));
        var playerId2 = new PlayerId(Guid.Parse("85029F22-6CBB-4DBC-AADF-981E2B332507"));
        var moveSequence = new MoveSequence(new[]
        {
            new Move(playerId1, Order.First),
            new Move(playerId2, Order.First.Next)
        });
        var moveOrderPolicy = new MoveOrderPolicy(moveSequence);
        return (moveOrderPolicy, playerId1, playerId2);
    }

    private static (
        MoveOrderPolicy Policy,
        PlayerId First, PlayerId Second, PlayerId Third) GivenPolicyWithThreePlayers()
    {
        var playerId1 = new PlayerId(Guid.Parse("1C963163-046E-4DC4-8DAD-34C0AA557F6A"));
        var playerId2 = new PlayerId(Guid.Parse("85029F22-6CBB-4DBC-AADF-981E2B332507"));
        var playerId3 = new PlayerId(Guid.Parse("056CF363-C071-46FA-8985-6F216EE96C80"));
        var moveSequence = new MoveSequence(new[]
        {
            new Move(playerId1, Order.First),
            new Move(playerId2, Order.First.Next),
            new Move(playerId3, Order.First.Next.Next)
        });
        var moveOrderPolicy = new MoveOrderPolicy(moveSequence);
        return (moveOrderPolicy, playerId1, playerId2, playerId3);
    }

    private static (
        MoveOrderPolicy Policy,
        PlayerId First, PlayerId Second, PlayerId Third, PlayerId Fourh) GivenPolicyWithFourPlayers()
    {
        var playerId1 = new PlayerId(Guid.Parse("1C963163-046E-4DC4-8DAD-34C0AA557F6A"));
        var playerId2 = new PlayerId(Guid.Parse("85029F22-6CBB-4DBC-AADF-981E2B332507"));
        var playerId3 = new PlayerId(Guid.Parse("056CF363-C071-46FA-8985-6F216EE96C80"));
        var playerId4 = new PlayerId(Guid.Parse("EF6C2416-CB88-4C31-B1C4-E8E5EFBF44BD"));
        var moveSequence = new MoveSequence(new[]
        {
            new Move(playerId1, Order.First),
            new Move(playerId2, Order.First.Next),
            new Move(playerId3, Order.First.Next.Next),
            new Move(playerId4, Order.First.Next.Next.Next)
        });
        var moveOrderPolicy = new MoveOrderPolicy(moveSequence);
        return (moveOrderPolicy, playerId1, playerId2, playerId3, playerId4);
    }
}