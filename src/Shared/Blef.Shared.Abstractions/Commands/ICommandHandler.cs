namespace Blef.Shared.Abstractions.Commands;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken cancellation);
}

public interface ICommandHandler<in TCommand, TCommandResult>
    where TCommand : ICommand
    where TCommandResult : ICommandResult
{
    Task<TCommandResult> Handle(TCommand command, CancellationToken cancellation);
}