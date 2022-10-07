using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class MakeNewGameHandler : ICommandHandler<MakeNewGame, MakeNewGame.Result>
{
    public async Task<MakeNewGame.Result> Handle(MakeNewGame command, CancellationToken cancellation)
    {
        var result = new MakeNewGame.Result(Guid.NewGuid());
        return await Task.FromResult(result);
    }
}