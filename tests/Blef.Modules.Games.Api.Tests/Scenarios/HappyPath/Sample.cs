namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

[UsesVerify]
public class Sample
{
    [Fact]
    public Task Test()
    {
        var person = ClassBeingTested.FindPerson();
        return Verify(person);
    }
}

public static class ClassBeingTested
{
    public static Person FindPerson() =>
        new()
        {
            Idx = Guid.NewGuid().ToString(),
            Idy = Guid.NewGuid().ToString(),
            Title = "Mr",
            GivenNames = "John",
            FamilyName = "Smith",
            Spouse = "Jill",
            Children = new[]
            {
                "Sam",
                "Mary"
            }
        };
}

public record Person()
{
    public string Idx { get; set; }
    public string Idy { get; set; }
    public string Title { get; set; }
    public string GivenNames { get; set; }
    public string FamilyName { get; set; }
    public string Spouse { get; set; }
    public string[] Children { get; set; }
}