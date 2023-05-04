using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events;

internal sealed record CheckPlaced(
    GameId Game,
    DealNumber Deal,
    PlayerId CheckingPlayer,
    LooserPlayer LooserPlayer) : IDomainEvent<CheckPlaced>;