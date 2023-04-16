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
        var actual = new LooserPlayer(guid);

        // assert
        Assert.Equal(guid, actual.PlayerId);
    }

    [Fact]
    public void CanCreateLooserPlayerWithEmptyGuidTest()
    {
        // arrange
        var emptyGuid = Guid.Empty;

        // act
        var actual = new LooserPlayer(emptyGuid);

        // assert
        Assert.Equal(emptyGuid, actual.PlayerId);
    }
}