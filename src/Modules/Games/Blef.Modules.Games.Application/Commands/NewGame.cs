using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record NewGame : ICommand<NewGame.Result>
{
    public sealed record Result(Guid GameId) : ICommandResult;
}