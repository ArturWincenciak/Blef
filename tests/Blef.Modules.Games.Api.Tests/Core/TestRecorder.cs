using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class TestRecorder
{
    private int _counter;
    private readonly List<TestResult> _items = new();

    public IEnumerable<TestResult> Actual => _items;

    public void Record(string action, object argument, IQueryResult result) =>
        _items.Add(new (++_counter, action, argument, result));

    public void Record(string action, object argument, ICommandResult result) =>
        _items.Add(new(++_counter, action, argument, result));

    internal record TestResult(int No, string Action, object Argument, object Result);

    internal record EmptyCommandResult(string Status) : ICommandResult;
}