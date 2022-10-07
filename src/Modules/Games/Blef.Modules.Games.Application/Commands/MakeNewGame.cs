using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record MakeNewGame : ICommand<MakeNewGame.Result>
{
    public sealed record Result(Guid GameId) : ICommandResult;
}