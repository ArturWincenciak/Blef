using System.Diagnostics;
using Blef.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blef.Shared.Infrastructure.Commands;

internal sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly ILogger<CommandDispatcher> _logger;
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider, ILogger<CommandDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task SendAsync(ICommand command)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);
            var handleMethod = handlerType.GetMethod(nameof(ICommandHandler<ICommand>.HandleAsync));
            Debug.Assert(handleMethod != null, nameof(handleMethod) + " != null");
            await (Task) handleMethod.Invoke(handler, new object?[] {command})!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}; Command: {{@command}}", command);
            throw;
        }
    }

    public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command) where TResult : ICommandResult
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);
            var method = handlerType.GetMethod(nameof(ICommandHandler<ICommand<TResult>, TResult>.HandleAsync));
            Debug.Assert(method != null, nameof(method) + " != null");
            var result = method.Invoke(handler, new object?[] { command });
            return await (result as Task<TResult>)!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{ex.Message}; Command: {{@command}}", command);
            throw;
        }
    }
}