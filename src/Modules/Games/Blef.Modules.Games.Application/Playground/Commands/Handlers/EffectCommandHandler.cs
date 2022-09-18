using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Playground.Commands.Handlers;

internal sealed class EffectCommandHandler : ICommandHandler<EffectCommand, EffectCommand.Result>
{
    public async Task<EffectCommand.Result> Handle(EffectCommand command, CancellationToken cancellation)
    {
        var collection = new string [command.Amount];
        Array.Fill(collection, command.Flag ? "Donald Knuth" : "Surreal Numbers");
        return await Task.FromResult(new EffectCommand.Result(collection));
    }
}