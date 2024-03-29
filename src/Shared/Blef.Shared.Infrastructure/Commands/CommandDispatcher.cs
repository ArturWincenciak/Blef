﻿using Blef.Shared.Abstractions.Commands;
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

    public async Task<TCommandResult> Dispatch<TCommand, TCommandResult>(
        TCommand command,
        CancellationToken cancellation)
        where TCommand : ICommand<TCommandResult>
        where TCommandResult : ICommandResult
    {
        try
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();
            return await handler.Handle(command, cancellation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: "{@Command}", command);
            throw;
        }
    }
}