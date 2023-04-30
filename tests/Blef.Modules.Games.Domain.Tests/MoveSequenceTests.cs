using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Tests;

public class MoveSequenceTests
{
    [Fact]
    public void CreateMoveSequenceWithTwoPlayersTest()
    {
        // act
        var actual = Record.Exception(() => new MoveSequence(new Move[]
        {
            new(new(Guid.Parse("3428CF5B-01B8-4345-B619-91F9DF201DEA")), Order.First),
            new(new(Guid.Parse("608C26DB-5647-4243-BFD3-5EDAA72726AA")), Order.First.Next),
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
            new(new(Guid.Parse("4407F648-FE6E-4BF9-B8DC-65257BBCAB8B")), Order.Create(1)),
            new(new(Guid.Parse("6EEAEAB2-50FC-4115-9770-733B0EFBB2C9")), Order.Create(2)),
            new(new(Guid.Parse("AB1500AE-85A4-47F1-BC32-D3F230C85340")), Order.Create(3)),
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
            new(new(Guid.Parse("594E5B30-1B57-4D20-9E7E-CF204DA0A923")), Order.Create(1)),
            new(new(Guid.Parse("0DA0B75D-9D63-42B1-805D-82348CA34604")), Order.Create(2)),
            new(new(Guid.Parse("D7AC7257-4039-42CB-B8D3-3E41DBA1CA83")), Order.Create(3)),
            new(new(Guid.Parse("422C7237-505B-4FE7-971F-F882EF2ECD6C")), Order.Create(4)),
        }));

        // assert
        Assert.Null(actual);
    }

    [Fact]
    public void CannotCreateMoveSequenceWithFivePlayersTest() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new MoveSequence(new Move[]
        {
            new(new(Guid.Parse("8DD981F1-A303-4070-B684-B30F45BAAB84")), Order.Create(1)),
            new(new(Guid.Parse("CD5D03BC-B243-4E6A-8809-6A6B6B234390")), Order.Create(2)),
            new(new(Guid.Parse("022BC534-8CAB-4CFA-A7FD-280DA9C2697A")), Order.Create(3)),
            new(new(Guid.Parse("8904618B-52D4-4785-BE4F-0106DEDA1463")), Order.Create(4)),
            new(new(Guid.Parse("DC54607E-76F3-48B8-936C-CE924551B436")), Order.Create(5)),
        }));

    [Fact]
    public void CannotCreateMoveSequenceWithNotUniquesPlayersTest() =>
        Assert.Throws<ArgumentException>(() =>
        {
            var playerId = "8FA39222-7D70-4D1D-B8DD-7F07320250FA";
            return new MoveSequence(new Move[]
            {
                new(new(Guid.Parse(playerId)), Order.Create(1)),
                new(new(Guid.Parse(playerId)), Order.Create(2)),
            });
        });

    [Fact]
    public void CannotCreateMoveSequenceWithNotUniquesOrdersTest() =>
        Assert.Throws<ArgumentException>(() =>
        {
            return new MoveSequence(new Move[]
            {
                new(new(Guid.Parse("CE16BDF3-70E0-4A04-A89E-C1EB29E1A886")), Order.First),
                new(new(Guid.Parse("682FD22D-D8C1-4F27-A3D8-40A0417E5447")), Order.First),
            });
        });

    [Fact]
    public void CannotCreateMoveSequenceWithNotOrderStartingFromFirstTest() =>
        Assert.Throws<ArgumentException>(() =>
        {
            return new MoveSequence(new Move[]
            {
                new(new(Guid.Parse("294B72A3-FB5D-45DE-ACDE-D1C7F1F02C3A")), Order.Create(2)),
                new(new(Guid.Parse("3F57BC59-28D7-44E5-B3AE-ABDD755C41E3")), Order.Create(3)),
            });
        });

    [Fact]
    public void CannotCreateMoveSequenceWithNotOrderedTest() =>
        Assert.Throws<ArgumentException>(() =>
        {
            return new MoveSequence(new Move[]
            {
                new(new(Guid.Parse("69D7417E-33C2-44CC-A24E-935179158F5F")), Order.Create(1)),
                new(new(Guid.Parse("184EAC4B-AF58-4F16-B5A8-110890D412AA")), Order.Create(2)),
                new(new(Guid.Parse("0092C215-939A-4FC8-9A27-F284410A4A52")), Order.Create(4))
            });
        });
}