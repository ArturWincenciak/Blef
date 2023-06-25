namespace Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

public record ProblemDetails(
    string Type,
    string Title,
    string Detail,
    string Instance,
    IDictionary<string, string[]> Errors);
