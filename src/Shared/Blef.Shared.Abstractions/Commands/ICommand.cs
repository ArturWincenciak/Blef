using System.Diagnostics.CodeAnalysis;

namespace Blef.Shared.Abstractions.Commands;

public interface ICommand
{
}

[SuppressMessage(category: "ReSharper", checkId: "UnusedTypeParameter")]
public interface ICommand<TCommandResult> : ICommand
    where TCommandResult : ICommandResult
{
}