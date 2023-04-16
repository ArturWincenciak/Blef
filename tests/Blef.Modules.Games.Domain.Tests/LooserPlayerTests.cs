using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Tests;

public class LooserPlayerTests
{
    [Fact]
    public void CreateLooserPlayerTest()
    {
        var exception = Record.Exception(() =>
        {
            // arrange
            var guid = Guid.Parse("07147D29-6F51-4D30-8470-8807CA29A5F0");

            // act
            var actual = new LooserPlayer(guid);

            // assert
            Assert.Equal(expected: guid, actual: actual.PlayerId);
        });
        Assert.Null(exception);
    }

    [Fact]
    public void CanCreateLooserPlayerWithEmptyGuidTest()
    {
        var exception = Record.Exception(() =>
        {
            var emptyGuid = Guid.Empty;
            var looserPlayer = new LooserPlayer(emptyGuid);
            Assert.Equal(expected: emptyGuid, looserPlayer.PlayerId);
        });
        Assert.Null(exception);
    }
}