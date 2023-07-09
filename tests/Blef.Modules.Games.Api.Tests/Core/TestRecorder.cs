using System.Net;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class TestRecorder
{
    private readonly List<TestResult> _items = new();
    private int _counter;

    public IReadOnlyCollection<TestResult> Actual => _items;

    public void Record(Request request, Response response) =>
        _items.Add(new TestResult(++_counter, request, response));

    internal sealed record TestResult(int No, Request Request, Response Response);

    internal sealed record Request(string Path, HttpMethod Method, object? Payload = null);

    internal sealed record Response(HttpStatusCode StatusCode, object? Payload = null);

    internal enum HttpMethod
    {
        Get = 1,
        Post = 2,
        Put = 4,
        Delete = 8,
    }
}