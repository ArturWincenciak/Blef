using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events;

internal sealed record GameOver(
    GameId Game,
    GamePlayer Winner) : IDomainEvent<GameOver>;