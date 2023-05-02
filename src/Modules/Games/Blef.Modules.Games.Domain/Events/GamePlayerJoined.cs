using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events;

internal sealed record GamePlayerJoined(Guid GameId, Guid PlayerId, string Nick) : IDomainEvent<GamePlayerJoined>;