using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class MoveSequenceTests
{
    [Fact]
    public void CreateMoveSequenceWithTwoPlayersTest()
    {
        // act
        var actual = Record.Exception(() => new MoveSequence(new Move[]
        {
            new(Player: new PlayerId(Guid.Parse("3428CF5B-01B8-4345-B619-91F9DF201DEA")), Order.First),
            new(Player: new PlayerId(Guid.Parse("608C26DB-5647-4243-BFD3-5EDAA72726AA")), Order.First.Next)
        }));

        // assert
        Assert.Null(actual);
    }

    [Fact]
    public void CannotCreateMoveSequenceWithNullMovesTest() =>
        Assert.Throws<ArgumentNullException>(() => new MoveSequence(null!));

    [Fact]
    public void CannotCreateMoveSequenceWithEmptyMovesTest() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new MoveSequence(Array.Empty<Move>()));


    [Fact]
    public void CreateMoveSequenceWithThreePlayersTest()
    {
        // act
        var actual = Record.Exception(() => new MoveSequence(new Move[]
        {
            new(Player: new PlayerId(Guid.Parse("4407F648-FE6E-4BF9-B8DC-65257BBCAB8B")), Order: Order.Create(1)),
            new(Player: new PlayerId(Guid.Parse("6EEAEAB2-50FC-4115-9770-733B0EFBB2C9")), Order: Order.Create(2)),
            new(Player: new PlayerId(Guid.Parse("AB1500AE-85A4-47F1-BC32-D3F230C85340")), Order: Order.Create(3))
        }));

        // assert
        Assert.Null(actual);
    }

    [Fact]
    public void CreateMoveSequenceWithFourPlayersTest()
    {
        // act
        var actual = Record.Exception(() => new MoveSequence(new Move[]
        {
            new(Player: new PlayerId(Guid.Parse("594E5B30-1B57-4D20-9E7E-CF204DA0A923")), Order: Order.Create(1)),
            new(Player: new PlayerId(Guid.Parse("0DA0B75D-9D63-42B1-805D-82348CA34604")), Order: Order.Create(2)),
            new(Player: new PlayerId(Guid.Parse("D7AC7257-4039-42CB-B8D3-3E41DBA1CA83")), Order: Order.Create(3)),
            new(Player: new PlayerId(Guid.Parse("422C7237-505B-4FE7-971F-F882EF2ECD6C")), Order: Order.Create(4))
        }));

        // assert
        Assert.Null(actual);
    }

    [Fact]
    public void CannotCreateMoveSequenceWithFivePlayersTest() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new MoveSequence(new Move[]
        {
            new(Player: new PlayerId(Guid.Parse("8DD981F1-A303-4070-B684-B30F45BAAB84")), Order: Order.Create(1)),
            new(Player: new PlayerId(Guid.Parse("CD5D03BC-B243-4E6A-8809-6A6B6B234390")), Order: Order.Create(2)),
            new(Player: new PlayerId(Guid.Parse("022BC534-8CAB-4CFA-A7FD-280DA9C2697A")), Order: Order.Create(3)),
            new(Player: new PlayerId(Guid.Parse("8904618B-52D4-4785-BE4F-0106DEDA1463")), Order: Order.Create(4)),
            new(Player: new PlayerId(Guid.Parse("DC54607E-76F3-48B8-936C-CE924551B436")), Order: Order.Create(5))
        }));

    [Fact]
    public void CannotCreateMoveSequenceWithNotUniquesPlayersTest() =>
        Assert.Throws<ArgumentException>(() =>
        {
            var playerId = "8FA39222-7D70-4D1D-B8DD-7F07320250FA";
            return new MoveSequence(new Move[]
            {
                new(Player: new PlayerId(Guid.Parse(playerId)), Order: Order.Create(1)),
                new(Player: new PlayerId(Guid.Parse(playerId)), Order: Order.Create(2))
            });
        });

    [Fact]
    public void CannotCreateMoveSequenceWithNotUniquesOrdersTest() =>
        Assert.Throws<ArgumentException>(() =>
        {
            return new MoveSequence(new Move[]
            {
                new(Player: new PlayerId(Guid.Parse("CE16BDF3-70E0-4A04-A89E-C1EB29E1A886")), Order.First),
                new(Player: new PlayerId(Guid.Parse("682FD22D-D8C1-4F27-A3D8-40A0417E5447")), Order.First)
            });
        });

    [Fact]
    public void CannotCreateMoveSequenceWithNotOrderStartingFromFirstTest() =>
        Assert.Throws<ArgumentException>(() =>
        {
            return new MoveSequence(new Move[]
            {
                new(Player: new PlayerId(Guid.Parse("294B72A3-FB5D-45DE-ACDE-D1C7F1F02C3A")), Order: Order.Create(2)),
                new(Player: new PlayerId(Guid.Parse("3F57BC59-28D7-44E5-B3AE-ABDD755C41E3")), Order: Order.Create(3))
            });
        });

    [Fact]
    public void CannotCreateMoveSequenceWithNotOrderedTest() =>
        Assert.Throws<ArgumentException>(() =>
        {
            return new MoveSequence(new Move[]
            {
                new(Player: new PlayerId(Guid.Parse("69D7417E-33C2-44CC-A24E-935179158F5F")), Order: Order.Create(1)),
                new(Player: new PlayerId(Guid.Parse("184EAC4B-AF58-4F16-B5A8-110890D412AA")), Order: Order.Create(2)),
                new(Player: new PlayerId(Guid.Parse("0092C215-939A-4FC8-9A27-F284410A4A52")), Order: Order.Create(4))
            });
        });

    [Fact]
    public void GetPlayersTest()
    {
        // arrange
        var playerId1 = new PlayerId(Guid.Parse("9BD2D1FD-8890-41F6-A53D-1E38470C38C0"));
        var playerId2 = new PlayerId(Guid.Parse("529968FC-FD7E-4607-882F-D3B81F01F022"));
        var playerId3 = new PlayerId(Guid.Parse("9B2D498B-509F-4BB3-BE16-1DEED38B59C8"));
        var moveSequence = new MoveSequence(new Move[]
        {
            new(playerId1, Order: Order.Create(1)),
            new(playerId2, Order: Order.Create(2)),
            new(playerId3, Order: Order.Create(3))
        });

        // act
        var actual = moveSequence.Players;

        // assert
        Assert.Equal(expected: new[] {playerId1, playerId2, playerId3}, actual);
    }

    [Fact]
    public void GetFirstMoveTest()
    {
        // arrange
        var moveSequence = new MoveSequence(new Move[]
        {
            new(Player: new PlayerId(Guid.Parse("D2EC0A5F-743A-4E78-9493-E8663BBA57ED")), Order: Order.Create(1)),
            new(Player: new PlayerId(Guid.Parse("E6006748-478C-49F2-A68E-F4EBEC0C88DA")), Order: Order.Create(2)),
            new(Player: new PlayerId(Guid.Parse("9F0153CC-25EA-4513-987C-C5C28D1C5DBB")), Order: Order.Create(3))
        });

        // act
        var actual = moveSequence.FirstMove;
        var expected = new Move(Player: new PlayerId(Guid.Parse("D2EC0A5F-743A-4E78-9493-E8663BBA57ED")),
            Order: Order.Create(1));

        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetLastMoveTest()
    {
        // arrange
        var moveSequence = new MoveSequence(new Move[]
        {
            new(Player: new PlayerId(Guid.Parse("B681BFD3-89DD-469F-B261-5AB63642517E")), Order: Order.Create(1)),
            new(Player: new PlayerId(Guid.Parse("A88EDE1A-1D5A-43B9-ACD5-2364A0D3D2CC")), Order: Order.Create(2)),
            new(Player: new PlayerId(Guid.Parse("3A26957A-5B27-4CAE-8549-90898742E52A")), Order: Order.Create(3))
        });

        // act
        var actual = moveSequence.LastMove;
        var expected = new Move(Player: new PlayerId(Guid.Parse("3A26957A-5B27-4CAE-8549-90898742E52A")),
            Order: Order.Create(3));

        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetMoveForPlayerTest()
    {
        // arrange
        var move1 = new Move(Player: new PlayerId(Guid.Parse("5E6DB9BD-BA5C-41FF-ADD9-8558215E0169")),
            Order: Order.Create(1));
        var move2 = new Move(Player: new PlayerId(Guid.Parse("A1B0A79D-CBAB-49A8-B563-01E8FED14BF9")),
            Order: Order.Create(2));
        var move3 = new Move(Player: new PlayerId(Guid.Parse("85F49851-884B-4DEF-A0C9-C142618BA87A")),
            Order: Order.Create(3));
        var move4 = new Move(Player: new PlayerId(Guid.Parse("BDBC0022-2F36-4542-B769-B06C58BFDB82")),
            Order: Order.Create(4));
        var moveSequence = new MoveSequence(new[] {move1, move2, move3, move4});

        // act
        var actual1 = moveSequence.GetMove(move1.Player);
        var actual2 = moveSequence.GetMove(move2.Player);
        var actual3 = moveSequence.GetMove(move3.Player);
        var actual4 = moveSequence.GetMove(move4.Player);

        // assert
        Assert.Equal(move1, actual1);
        Assert.Equal(move2, actual2);
        Assert.Equal(move3, actual3);
        Assert.Equal(move4, actual4);
    }


    [Fact]
    public void GetMoveForOrderTest()
    {
        // arrange
        var move1 = new Move(Player: new PlayerId(Guid.Parse("CD2542C9-80E9-4F08-A320-E9965B563473")),
            Order: Order.Create(1));
        var move2 = new Move(Player: new PlayerId(Guid.Parse("7DB0A93B-1501-4009-AECF-8883DC7FD99B")),
            Order: Order.Create(2));
        var move3 = new Move(Player: new PlayerId(Guid.Parse("41692A9F-4785-4B1A-8552-0FE2AC6E6CC8")),
            Order: Order.Create(3));
        var move4 = new Move(Player: new PlayerId(Guid.Parse("1B568718-EC5D-4388-9D34-52A09B6AD045")),
            Order: Order.Create(4));
        var moveSequence = new MoveSequence(new[] {move1, move2, move3, move4});

        // act
        var actual1 = moveSequence.GetMove(move1.Order);
        var actual2 = moveSequence.GetMove(move2.Order);
        var actual3 = moveSequence.GetMove(move3.Order);
        var actual4 = moveSequence.GetMove(move4.Order);

        // assert
        Assert.Equal(move1, actual1);
        Assert.Equal(move2, actual2);
        Assert.Equal(move3, actual3);
        Assert.Equal(move4, actual4);
    }
}