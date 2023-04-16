using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Tests;

public class LooserPlayerTests
{
    [Fact]
    public void CreateLooserPlayerTest()
    {
        var guid = Guid.Parse("07147D29-6F51-4D30-8470-8807CA29A5F0");
        var exception = Record.Exception(() => new LooserPlayer(guid));
        Assert.Null(exception);
    }

    [Fact]
    public void CanCreateLooserPlayerWithEmptyGuidTest()
    {
        var emptyGuid = Guid.Empty;
        var exception = Record.Exception(() => new LooserPlayer(emptyGuid));
        Assert.Null(exception);
    }
}