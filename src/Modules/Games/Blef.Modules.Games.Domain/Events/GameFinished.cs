using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events;

internal sealed record GameFinished(
    GameId Game,
    GamePlayer Winner) : IDomainEvent<GameFinished>;