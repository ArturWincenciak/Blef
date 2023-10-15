using System.Diagnostics.CodeAnalysis;

namespace Blef.Shared.Abstractions.Commands;

[SuppressMessage(category: "ReSharper", checkId: "UnusedMember.Global")]
public interface ICommandDispatcher
{
    Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
        where TCommand : ICommand<TCommandResult>
        where TCommandResult : ICommandResult;
}