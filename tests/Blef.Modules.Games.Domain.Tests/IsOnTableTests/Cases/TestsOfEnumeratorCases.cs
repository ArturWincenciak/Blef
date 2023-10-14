namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

public class TestsOfEnumeratorCases
{
    [Fact]
    public void GetPokerHandIsOnTheTableCasesEnumeratorTest()
    {
        using var enumerator = new PokerHandIsOnTheTableCases().GetEnumerator();
        Assert.NotNull(enumerator);
    }

    [Fact]
    public void GetPokerHandIsNotOnTheTableCasesEnumeratorTest()
    {
        using var enumerator = new PokerHandIsNotOnTheTableCases().GetEnumerator();
        Assert.NotNull(enumerator);
    }
}