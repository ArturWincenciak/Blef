using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record JoinGame(Guid GameId, string Nick) : ICommand<JoinGame.Result>
{
    public sealed record Result(Guid PlayerId, string Nick) : ICommandResult;
}