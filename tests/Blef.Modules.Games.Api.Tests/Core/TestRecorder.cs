using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class TestRecorder
{
    private readonly List<TestResult> _items = new();
    private int _counter;

    public IEnumerable<TestResult> Actual => _items;

    public void Record(string action, object argument, IQueryResult result) =>
        _items.Add(new TestResult(++_counter, action, argument, result));

    public void Record(string action, object argument, ICommandResult result) =>
        _items.Add(new TestResult(++_counter, action, argument, result));

    internal record TestResult(int No, string Action, object Argument, object Result);

    internal record EmptyCommandResult(string Status) : ICommandResult;
}