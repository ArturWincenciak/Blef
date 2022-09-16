namespace Blef.Shared.Abstractions.Commands;

public interface ICommandDispatcher
{
    Task SendAsync(ICommand command);
    Task<TResult> SendAsync<TResult>(ICommand<TResult> command) where TResult : ICommandResult;
}