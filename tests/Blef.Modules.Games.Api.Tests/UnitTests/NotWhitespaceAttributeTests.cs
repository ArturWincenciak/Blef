using Blef.Modules.Games.Api.Controllers.Games.Validators;

namespace Blef.Modules.Games.Api.Tests.UnitTests;

public class NotWhitespaceAttributeTests
{
    [Fact]
    public void Valid()
    {
        // arrange
        var target = new NotWhitespaceAttribute();
        const string payload = "not whitespace text";

        // act
        var actual = target.IsValid(payload);

        // assert
        Assert.True(actual);
    }

    [Fact]
    public void Null()
    {
        // arrange
        var target = new NotWhitespaceAttribute();
        string? nullPayload = null;

        // act
        var actual = target.IsValid(nullPayload);

        // assert
        Assert.False(actual);
    }

    [Fact]
    public void Whitespace()
    {
        // arrange
        var target = new NotWhitespaceAttribute();
        const string whitespacePayload = "     ";

        // act
        var actual = target.IsValid(whitespacePayload);

        // assert
        Assert.False(actual);
    }

    [Fact]
    public void Empty()
    {
        // arrange
        var target = new NotWhitespaceAttribute();
        var emptyPayload = string.Empty;

        // act
        var actual = target.IsValid(emptyPayload);

        // assert
        Assert.False(actual);
    }

    [Fact]
    public void NotString()
    {
        // arrange
        var target = new NotWhitespaceAttribute();
        const int notStringPayload = 123;

        // act
        var actual = target.IsValid(notStringPayload);

        // assert
        Assert.False(actual);
    }
}