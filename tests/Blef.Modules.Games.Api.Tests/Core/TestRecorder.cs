using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class TestRecorder
{
    private readonly List<TestResult> _items = new();
    private int _counter;

    public IReadOnlyCollection<TestResult> Actual => _items;

    public void Record(string action, object argument, object result) =>
        _items.Add(new TestResult(++_counter, action, argument, result));

    [UsedImplicitly]
    internal record TestResult(int No, string Action, object Argument, object Result);

    [UsedImplicitly]
    internal record EmptyCommandResult(string Status) : ICommandResult;
}