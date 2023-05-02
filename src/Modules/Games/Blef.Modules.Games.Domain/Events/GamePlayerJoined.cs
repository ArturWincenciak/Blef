using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events;

internal sealed record GamePlayerJoined(PlayerId PlayerId, PlayerNick Nick) : IDomainEvent<GamePlayerJoined>;