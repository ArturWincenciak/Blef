namespace Blef.Shared.Abstractions.Commands;

public interface ICommand { }

public interface ICommand<TCommandResult> : ICommand
    where TCommandResult : ICommandResult { }