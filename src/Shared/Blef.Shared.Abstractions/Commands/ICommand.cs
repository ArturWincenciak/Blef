namespace Blef.Shared.Abstractions.Commands;

public interface ICommand
{
}

// ReSharper disable once UnusedTypeParameter
public interface ICommand<TCommandResult> : ICommand
    where TCommandResult : ICommandResult
{
}