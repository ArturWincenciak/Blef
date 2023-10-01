using System.Net;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class TestRecorder
{
    private readonly List<StepResult> _items = new();
    private int _counter;
    private string? _scenarioDescription;

    public TestResult Actual => new (_scenarioDescription, _items);

    public void WithScenarioDescription(string description) =>
        _scenarioDescription = description;

    public void Record(Request request, Response response, string? description = null) =>
        _items.Add(new StepResult(++_counter, description, request, response));

    internal sealed record StepResult(int No, string? Description, Request Request, Response Response);

    internal sealed record TestResult(string? Description, IReadOnlyCollection<StepResult> Steps);

    internal sealed record Request(string Path, HttpMethod Method, object? Payload = null);

    internal sealed record Response(HttpStatusCode StatusCode, object? Payload = null);

    internal enum HttpMethod
    {
        Get = 1,
        Post = 2,
        Put = 4,
        Delete = 8
    }
}