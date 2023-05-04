using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Tests;

public class LooserPlayerTests
{
    [Fact]
    public void CreateLooserPlayerTest()
    {
        // arrange
        var guid = Guid.Parse("07147D29-6F51-4D30-8470-8807CA29A5F0");

        // act
        var actual = new LooserPlayer(new(guid));

        // assert
        Assert.Equal(guid, actual.Player.Id);
        Assert.True(new LooserPlayer(new(guid)) == actual);
    }
}