namespace Blef.Shared.Abstractions.Commands;

public interface ICommandDispatcher
{
    Task Dispatch<TCommand>(TCommand command, CancellationToken cancellation)
        where TCommand : ICommand;

    Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
        where TCommand : ICommand<TCommandResult>
        where TCommandResult : ICommandResult;
}