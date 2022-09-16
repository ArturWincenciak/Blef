namespace Blef.Shared.Abstractions.Commands;

public interface ICommand { }

public interface ICommand<TResult> : ICommand
    where TResult : ICommandResult
{ }