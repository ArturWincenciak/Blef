using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands;

public sealed record JoinGame(GameId GameId, string Nick) : ICommand<JoinGame.Result>
{
    [UsedImplicitly]
    public sealed record Result(Guid PlayerId, string Nick) : ICommandResult;
}