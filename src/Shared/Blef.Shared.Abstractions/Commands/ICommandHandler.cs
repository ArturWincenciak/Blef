using System.Diagnostics.CodeAnalysis;

namespace Blef.Shared.Abstractions.Commands;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken cancellation);
}

public interface ICommandHandler<in TCommand, TCommandResult>
    where TCommand : ICommand<TCommandResult>
    where TCommandResult : ICommandResult
{
    [SuppressMessage(category: "ReSharper", checkId: "UnusedParameter.Global")]
    Task<TCommandResult> Handle(TCommand command, CancellationToken cancellation);
}