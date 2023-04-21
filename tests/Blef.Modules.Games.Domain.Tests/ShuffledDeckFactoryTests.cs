using Blef.Modules.Games.Domain.Services;

namespace Blef.Modules.Games.Domain.Tests;

public class ShuffledDeckFactoryTests
{
    [Fact]
    public void CreateDeckByFactoryTest() =>
        Assert.Null(Record.Exception(() =>
        {
            var factory = new ShuffledDeckFactory();
            return factory.Create();
        }));
}