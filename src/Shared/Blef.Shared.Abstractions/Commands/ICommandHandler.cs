namespace Blef.Shared.Abstractions.Commands;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}

public interface ICommandHandler<in TCommand, TResult>
    where TCommand : ICommand
    where TResult : ICommandResult
{
    Task<TResult> HandleAsync(TCommand command);
}